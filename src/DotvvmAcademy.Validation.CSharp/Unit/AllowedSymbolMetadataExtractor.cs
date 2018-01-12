using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class AllowedSymbolMetadataExtractor : IMetadataExtractor
    {
        public void ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects, CSharpStaticAnalysisContext context)
        {
            context.AddMetadata<AllowedSymbolAnalyzer>(csharpObjects.Keys.ToImmutableHashSet());
        }
    }
}