using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public interface IMetadataExtractor
    {
        void ExtractMetadata(ImmutableDictionary<string, ICSharpObject> csharpObjects, CSharpStaticAnalysisContext context);
    }
}