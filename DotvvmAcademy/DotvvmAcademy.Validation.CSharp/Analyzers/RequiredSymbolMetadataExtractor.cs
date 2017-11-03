using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public class RequiredSymbolMetadataExtractor : IMetadataExtractor
    {
        public ImmutableDictionary<string, IValidationAnalyzerMetadata> ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects)
        {
            
        }
    }
}