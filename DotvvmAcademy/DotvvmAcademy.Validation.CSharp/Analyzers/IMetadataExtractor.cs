using DotvvmAcademy.Validation.CSharp.Abstractions;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public interface IMetadataExtractor
    {
        void ExtractMetadata(CSharpStaticAnalysisContext context, ImmutableDictionary<string, ICSharpObject> csharpObjects);
    }
}