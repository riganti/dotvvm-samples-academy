using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public static class AttributeExtractorExtensions
    {
        public static IEnumerable<TAttribute> Extract<TAttribute>(this IAttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.Extract(typeof(TAttribute), symbol).OfType<TAttribute>();
        }

        public static IEnumerable<AttributeData> ExtractAttributeData<TAttribute>(this IAttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.ExtractAttributeData(typeof(TAttribute), symbol);
        }

        public static bool HasAttribute<TAttribute>(this IAttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.HasAttribute(typeof(TAttribute), symbol);
        }
    }
}