using DotvvmAcademy.Validation.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidator : IValidator<CSharpValidationRequest, CSharpValidationResponse>
    {
        public ImmutableArray<DiagnosticAnalyzer> Analyzers = new ImmutableArray<DiagnosticAnalyzer>()
        {
            new RequiredMemberAnalyzer()
        };

        public CSharpValidator(ImmutableDictionary<string, CSharpResolvedValidationMethod> resolvedMethods)
        {
            ResolvedMethods = resolvedMethods;
        }

        public CompilationWithAnalyzersOptions Options { get; } = new CompilationWithAnalyzersOptions(null, null, false, false);

        public ImmutableDictionary<string, CSharpResolvedValidationMethod> ResolvedMethods { get; }

        public async Task<CSharpValidationResponse> Validate(CSharpValidationRequest request)
        {
            ClearAnalyzerContext();
            var response = new CSharpValidationResponse();
            var validatedTrees = request.ValidationUnits.Select(u => u.SyntaxTree).ToImmutableArray();
            var method = request.ValidationUnits
                .SelectMany(u => u.ValidationMethods)
                .Select(m => ResolvedMethods[m])
                .Aggregate((first, second) => first.Merge(second));
            var context = new CSharpValidationContext(request, response, method, validatedTrees);
            SetAnalyzerContext(context);
            var compilation = new CompilationWithAnalyzers(request.Compilation, Analyzers, Options);
            var diagnostics = await compilation.GetAllDiagnosticsAsync();
            response.Diagnostics = ConvertDiagnostics(diagnostics);
            return response;
        }

        private void ClearAnalyzerContext()
        {
            foreach (var analyzer in Analyzers.OfType<ValidationAnalyzer>())
            {
                analyzer.Context = null;
            }
        }

        private ImmutableArray<ValidationDiagnostic> ConvertDiagnostics(ImmutableArray<Diagnostic> diagnostics)
        {
            var validationDiagnostics = new List<ValidationDiagnostic>();
            for (int i = 0; i < diagnostics.Length; i++)
            {
                var diagnostic = diagnostics[i];
                var location = diagnostic.Location.Kind == LocationKind.None ? DiagnosticLocation.None : new DiagnosticLocation(diagnostic.Location.SourceSpan.Start, diagnostic.Location.SourceSpan.End);
                var validationDiagnostic = new ValidationDiagnostic(diagnostic.Id, diagnostic.GetMessage(), location);
                validationDiagnostics.Add(validationDiagnostic);
            }
            return validationDiagnostics.ToImmutableArray();
        }

        private void SetAnalyzerContext(CSharpValidationContext context)
        {
            foreach (var analyzer in Analyzers.OfType<ValidationAnalyzer>())
            {
                analyzer.Context = context;
            }
        }
    }
}