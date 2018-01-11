using DotvvmAcademy.CommonMark.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.CommonMark
{
    public class SegmentizedConverterBuilder
    {
        private readonly List<IPlaceholderParser> placeholderParsers = new List<IPlaceholderParser>();

        public void UsePlaceholderParser<TParser>()
            where TParser : class, IPlaceholderParser, new()
        {
            placeholderParsers.Add(new TParser());
        }

        public void UsePlaceholderParser(IPlaceholderParser parser)
        {
            placeholderParsers.Add(parser);
        }

        public SegmentizedConverter Build()
        {
            var converter = new SegmentizedConverter(placeholderParsers);
            return converter;
        }
    }
}
