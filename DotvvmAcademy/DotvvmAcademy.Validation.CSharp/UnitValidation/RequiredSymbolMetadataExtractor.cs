using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class RequiredSymbolMetadataExtractor : IMetadataExtractor
    {
        public void ExtractMetadata(ICSharpFactory factory, CSharpValidationRequest request)
        {
            var all = factory.GetAllObjects();
            var builder = ImmutableDictionary.CreateBuilder<string, RequiredSymbolMetadata>();
            foreach (var pair in all)
            {
                builder.Add(pair.Key, new RequiredSymbolMetadata { PossibleKind = pair.Value.GetRepresentingKind() });
            }
            request.StaticAnalysis.AddMetadata(builder.ToImmutable());
        }
    }
}