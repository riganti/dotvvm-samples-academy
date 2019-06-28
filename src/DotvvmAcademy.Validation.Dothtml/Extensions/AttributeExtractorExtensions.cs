using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public static class AttributeExtractorExtensions
    {
        internal static MarkupOptionsAttribute ExtractMarkupOptions(this IAttributeExtractor extractor, ISymbol symbol)
        {
            return extractor.Extract<MarkupOptionsAttribute>(symbol).SingleOrDefault() ?? new MarkupOptionsAttribute();
        }
    }
}