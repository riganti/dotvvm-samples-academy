using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    public static class AttributeExtractorExtensions
    {
        public static Attribute GetAttribute(this AttributeExtractor extractor, Type attributeType, ISymbol symbol)
        {
            return extractor.GetAttributes(attributeType, symbol).SingleOrDefault();
        }

        public static TAttribute GetAttribute<TAttribute>(this AttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return (TAttribute)extractor.GetAttribute(typeof(TAttribute), symbol);
        }

        public static ImmutableArray<AttributeData> GetAttributeData<TAttribute>(this AttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.GetAttributeData(typeof(TAttribute), symbol);
        }

        public static ImmutableArray<TAttribute> GetAttributes<TAttribute>(this AttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.GetAttributes(typeof(TAttribute), symbol).Cast<TAttribute>().ToImmutableArray();
        }

        public static bool HasAttribute<TAttribute>(this AttributeExtractor extractor, ISymbol symbol)
            where TAttribute : Attribute
        {
            return extractor.HasAttribute(typeof(TAttribute), symbol);
        }
    }
}