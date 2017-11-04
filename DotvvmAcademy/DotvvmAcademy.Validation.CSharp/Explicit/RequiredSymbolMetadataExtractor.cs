using DotvvmAcademy.Validation.CSharp.Abstractions;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class RequiredSymbolMetadataExtractor : IMetadataExtractor
    {
        public void ExtractMetadata(CSharpStaticAnalysisContext context, ImmutableDictionary<string, ICSharpObject> csharpObjects)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, RequiredSymbolMetadata>();
            foreach (var pair in csharpObjects)
            {
                builder.Add(pair.Key, new RequiredSymbolMetadata { PossibleKind = pair.Value.GetRepresentingKind() });
            }
            context.AddMetadata(builder.ToImmutable());
        }
    }
}