using CommonMark;
using CommonMark.Formatters;
using DotvvmAcademy.CommonMark.ComponentParsers;
using DotvvmAcademy.CommonMark.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CommonMark
{
    public class ComponentizedConverter
    {
        private CommonMarkSettings settings;
        private int lastWriterLength = 0;
        private List<ICommonMarkComponent> components = new List<ICommonMarkComponent>();
        private ComponentParser firstParser;
        private List<Type> componentParserTypes = new List<Type>();

        public ComponentizedConverter()
        {
            settings = CommonMarkSettings.Default.Clone();
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

        public Task<List<ICommonMarkComponent>> Convert(string markdown)
        {
            return Task.Run(() =>
            {
                lastWriterLength = 0;
                components.Clear();
                firstParser = ConstructParsers();
                var parsedMarkdown = CommonMarkConverter.Parse(markdown, settings);
                using (var writer = new StringWriter())
                {
                    CommonMarkConverter.ProcessStage3(parsedMarkdown, writer, settings);
                    if (TryParseHtmlLiteral(writer, out var htmlLiteral))
                    {
                        components.Add(htmlLiteral);
                    }
                }
                return components;
            });
        }

        public void Use<TComponentParser>()
            where TComponentParser : ComponentParser
        {
            componentParserTypes.Add(typeof(TComponentParser));
        }

        private bool TryParseHtmlLiteral(StringWriter writer, out HtmlLiteralComponent htmlLiteral)
        {
            writer.Flush();
            var html = writer.ToString();
            var htmlLiteralSource = html.Substring(lastWriterLength);
            lastWriterLength = html.Length;
            if (string.IsNullOrEmpty(htmlLiteralSource))
            {
                htmlLiteral = null;
                return false;
            }

            htmlLiteral = new HtmlLiteralComponent()
            {
                Source = htmlLiteralSource
            };
            return true;
        }

        private void ParsePlaceholder(string placeholder, StringWriter writer)
        {
            var component = firstParser.Parse(placeholder);
            if (component != null)
            {
                if (TryParseHtmlLiteral(writer, out var htmlLiteral))
                {
                    components.Add(htmlLiteral);
                }
                components.Add(component);
            }
        }

        private ComponentParser ConstructParsers()
        {
            ComponentParser next = new EndComponentParser();
            for (int i = componentParserTypes.Count - 1; i >= 0; i--)
            {
                var type = componentParserTypes[i];
                var current = (ComponentParser)Activator.CreateInstance(type, next);
                next = current;
            }
            return next;
        }
    }
}