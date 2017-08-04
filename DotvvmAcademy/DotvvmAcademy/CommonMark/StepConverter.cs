using CommonMark;
using CommonMark.Formatters;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace DotvvmAcademy.CommonMark
{
    public class StepConverter
    {
        private IDotvvmRequestContext context;
        private List<IControlParser<DotvvmControl>> parsers = new List<IControlParser<DotvvmControl>>();
        private CommonMarkSettings settings;
        private string source;

        public StepConverter(string source)
        {
            this.source = source;
            settings = CommonMarkSettings.Default.Clone();
            settings.AdditionalFeatures |= CommonMarkAdditionalFeatures.PlaceholderBracket;
            settings.OutputDelegate = (doc, output, s) =>
            {
                var formatter = new HtmlFormatter(output, s);
                formatter.PlaceholderResolver = Parse;
                formatter.WriteDocument(doc);
            };
        }

        public Action<DotvvmControl> ControlCreatedCallback { get; set; }

        public void RegisterParser(IControlParser<DotvvmControl> parser) => parsers.Add(parser);

        public string Render(IDotvvmRequestContext context)
        {
            this.context = context;
            return CommonMarkConverter.Convert(source, settings);
        }

        private string Parse(string placeholder)
        {
            using (var stringWriter = new StringWriter())
            {
                var htmlWriter = new HtmlWriter(stringWriter, context);
                var element = XElement.Parse(placeholder);
                foreach (var parser in parsers)
                {
                    if (parser.CanParse(element))
                    {
                        var control = parser.Parse(element);
                        ControlCreatedCallback(control);
                        control.Render(htmlWriter, context);
                        stringWriter.Flush();
                        return stringWriter.ToString();
                    }
                }
                return null;
            }
        }
    }
}