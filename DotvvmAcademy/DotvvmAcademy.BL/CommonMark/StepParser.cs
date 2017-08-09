using CommonMark;
using CommonMark.Formatters;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace DotvvmAcademy.BL.CommonMark
{
    public class StepParser
    {
        private List<SourceComponent> parsedComponents;
        private List<IComponentParser<SourceComponent>> parsers = new List<IComponentParser<SourceComponent>>();
        private CommonMarkSettings settings;
        private int lessonIndex;
        private string language;
        private int index;
        private int literalLength = 0;

        public StepParser()
        {
            settings = CommonMarkSettings.Default.Clone();
            settings.AdditionalFeatures |= CommonMarkAdditionalFeatures.PlaceholderBracket;
            settings.OutputDelegate = (doc, output, s) =>
            {
                var formatter = new HtmlFormatter(output, s);
                formatter.PlaceholderResolver = p => ResolvePlaceholder(p, (StringWriter)output);
                formatter.WriteDocument(doc);
            };
        }

        public StepDTO Parse(int lessonIndex, string language, int index, string source)
        {
            // create a new collection for this parsing
            parsedComponents = new List<SourceComponent>();
            this.lessonIndex = lessonIndex;
            this.language = language;
            this.index = index;

            var parsedMarkdown = CommonMarkConverter.Parse(source, settings);
            using (var writer = new StringWriter())
            {
                CommonMarkConverter.ProcessStage3(parsedMarkdown, writer, settings);
                var lastLiteral = ParseLiteral(writer);
                if(lastLiteral != null)
                {
                    parsedComponents.Add(lastLiteral);
                }
            }
            return new StepDTO(lessonIndex, language, index)
            {
                Source = parsedComponents
            };
        }

        public void RegisterComponentParser(IComponentParser<SourceComponent> parser) => parsers.Add(parser);

        private string ResolvePlaceholder(string placeholder, StringWriter writer)
        {
            var element = XElement.Parse(placeholder);
            foreach (var parser in parsers)
            {
                if (parser.CanParse(element))
                {
                    var literal = ParseLiteral(writer);
                    if(literal != null)
                    {
                        parsedComponents.Add(literal);
                    }

                    parsedComponents.Add(parser.Parse(element, lessonIndex, language, index));

                    // parsing was successful therefore remove the placeholder
                    return "";
                }
            }
            // parsing was not successful the placeholder stays
            return null;
        }

        private HtmlLiteralComponent ParseLiteral(StringWriter writer)
        {
            writer.Flush();
            var wholeSource = writer.ToString();
            var literalSource = wholeSource.Substring(literalLength);
            literalLength = wholeSource.Length;
            if(string.IsNullOrEmpty(literalSource))
            {
                return null;
            }
            var literal = new HtmlLiteralComponent(lessonIndex, language, index)
            {
                Source = literalSource
            };
            return literal;
        }
    }
}