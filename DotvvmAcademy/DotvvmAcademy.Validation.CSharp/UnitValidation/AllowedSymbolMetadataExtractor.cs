using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class AllowedSymbolMetadataExtractor : IMetadataExtractor
    {
        public void ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects, CSharpStaticAnalysisContext context)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, AllowedSymbolMetadata>();
            foreach (var pair in csharpObjects)
            {
                builder.Add(pair.Key, new AllowedSymbolMetadata());
            }
            context.AddMetadata(builder.ToImmutable());
        }
    }
}