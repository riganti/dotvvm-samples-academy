using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Tests
{
    public class AnalyzerTestBase : CSharpTestBase
    {
        public Task<ImmutableArray<Diagnostic>> TestAnalyzer(DiagnosticAnalyzer analyzer, string sample)
        {
            var compilation = GetCompilation(sample);
            var withAnalyzers = new CompilationWithAnalyzers(compilation,
                ImmutableArray.Create(analyzer),
                new CompilationWithAnalyzersOptions(null, null, false, false));
            return withAnalyzers.GetAnalyzerDiagnosticsAsync();
        }
    }
}