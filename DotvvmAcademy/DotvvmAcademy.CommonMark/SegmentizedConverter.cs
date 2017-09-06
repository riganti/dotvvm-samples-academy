using CommonMark;
using CommonMark.Formatters;
using DotvvmAcademy.CommonMark.Parsers;
using DotvvmAcademy.CommonMark.Segments;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CommonMark
{
    public class SegmentizedConverter
    {
        private readonly List<IPlaceholderParser> placeholderParsers;
        private readonly List<ISegment> segments = new List<ISegment>();
        private CommonMarkSettings settings;
        private int writerPosition;

        internal SegmentizedConverter(List<IPlaceholderParser> placeholderParsers)
        {
            this.placeholderParsers = placeholderParsers;
            SetSettings();
        }

        public async Task<IEnumerable<ISegment>> Convert(string markdown)
        {
            Reset();
            var parsedMarkdown = CommonMarkConverter.Parse(markdown, settings);
            using (var writer = new StringWriter())
            {
                CommonMarkConverter.ProcessStage3(parsedMarkdown, writer, settings);
                var htmlSegment = await ParseHtmlSegment(writer);
                if (htmlSegment != null)
                {
                    segments.Add(htmlSegment);
                }
            }
            return segments;
        }

        private async Task<HtmlSegment> ParseHtmlSegment(StringWriter writer)
        {
            await writer.FlushAsync();
            var writtenString = writer.ToString();
            var htmlSource = writtenString.Substring(writerPosition + 1);
            writerPosition = writtenString.Length - 1;
            if (string.IsNullOrEmpty(htmlSource))
            {
                return null;
            }

            return new HtmlSegment
            {
                Source = htmlSource
            };
        }

        private void ParsePlaceholder(string placeholder, StringWriter writer)
        {
            var context = new PlaceholderParsingContext(placeholder, segments, writer);
            for (int i = 0; i < placeholderParsers.Count; i++)
            {
                var handled = placeholderParsers[i].Parse(context).Result;
                if (handled)
                {
                    var htmlSegment = ParseHtmlSegment(writer).Result;
                    segments.Add(htmlSegment);
                    break;
                }
            }
        }

        private void Reset()
        {
            writerPosition = 0;
            segments.Clear();
        }

        private void SetOutputDelegate()
        {
            settings.AdditionalFeatures |= CommonMarkAdditionalFeatures.PlaceholderBracket;
            settings.OutputDelegate = (block, output, s) =>
            {
                var formatter = new HtmlFormatter(output, s);
                formatter.PlaceholderResolver = p =>
                {
                    ParsePlaceholder(p, (StringWriter)output);
                    return "";
                };
                formatter.WriteDocument(block);
            };
        }

        private void SetSettings()
        {
            settings = CommonMarkSettings.Default.Clone();
            if (placeholderParsers.Count > 0)
            {
                SetOutputDelegate();
            }
        }
    }
}