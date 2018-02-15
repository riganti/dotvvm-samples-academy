using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Tests
{
    public class AnalyzerTestBase : CSharpTestBase
    {
        public Task<ImmutableArray<Diagnostic>> TestAnalyzer(Compilation compilation, DiagnosticAnalyzer analyzer)
        {
            var withAnalyzers = new CompilationWithAnalyzers(compilation,
                ImmutableArray.Create(analyzer),
                new CompilationWithAnalyzersOptions(null, null, false, false));
            return withAnalyzers.GetAnalyzerDiagnosticsAsync();
        }
    }
}