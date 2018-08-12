using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public static class TypedAttributeExtractorExtensions
    {
        public static ImmutableArray<TAttribute> Extract<TAttribute>(this ITypedAttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.Extract(typeof(TAttribute), symbol).OfType<TAttribute>().ToImmutableArray();
        }

        public static ImmutableArray<AttributeData> ExtractRoslyn<TAttribute>(this ITypedAttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.ExtractRoslyn(typeof(TAttribute), symbol);
        }

        public static bool HasAttribute<TAttribute>(this ITypedAttributeExtractor extractor, ISymbol symbol)
                    where TAttribute : Attribute
        {
            return extractor.HasAttribute(typeof(TAttribute), symbol);
        }
    }
}