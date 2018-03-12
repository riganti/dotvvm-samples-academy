using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class AnalyzerTestBase : CSharpTestBase
    {
        public AnalyzerTestBase()
        {
            Factory = new MetadataNameFactory(DefaultFormatter, ReflectionFormatter, UserFriendlyFormatter);
        }

        public MetadataNameFormatter DefaultFormatter { get; } = new MetadataNameFormatter();

        public MetadataNameFactory Factory { get; }

        public ReflectionMetadataNameFormatter ReflectionFormatter { get; } = new ReflectionMetadataNameFormatter();

        public UserFriendlyMetadataNameFormatter UserFriendlyFormatter { get; } = new UserFriendlyMetadataNameFormatter();

        public Task<ImmutableArray<Diagnostic>> TestAnalyzer(Compilation compilation, DiagnosticAnalyzer analyzer)
        {
            var withAnalyzers = new CompilationWithAnalyzers(compilation,
                ImmutableArray.Create(analyzer),
                new CompilationWithAnalyzersOptions(null, null, false, false));
            return withAnalyzers.GetAnalyzerDiagnosticsAsync();
        }
    }
}