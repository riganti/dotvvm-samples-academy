using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public interface IMetadataExtractor
    {
        Type ExtractedMetadataType { get; }

        void ExtractMetadata(CSharpStaticAnalysisContext context, ImmutableDictionary<string, ICSharpObject> csharpObjects);
    }
}