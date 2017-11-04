using DotvvmAcademy.Validation.CSharp.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
using DotvvmAcademy.Validation.CSharp.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidator : ICSharpValidator
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ImmutableArray<ValidationAnalyzer> analyzers;
        private CompilationWithAnalyzersOptions options = new CompilationWithAnalyzersOptions(null, null, false, false);

        protected internal DefaultCSharpValidator(IServiceProvider serviceProvider, IEnumerable<ValidationAnalyzer> analyzers)
        {
            this.serviceProvider = serviceProvider;
            this.analyzers = analyzers.ToImmutableArray();
        }

        public ImmutableDictionary<string, CSharpStaticAnalysisContext> StaticAnalysisContexts { private get; set; }

        public async Task<CSharpValidationResponse> Validate(CSharpValidationRequest request)
        {
            var response = new CSharpValidationResponse();
            ClearAnalyzerContext();
            try
            {
                var contexts = request.ValidationUnits
                    .SelectMany(u => u.ValidationMethods)
                    .Select(m => StaticAnalysisContexts[m]);
                var context = CSharpStaticAnalysisContext.Merge(contexts);
                context.ValidatedTrees = request.ValidationUnits.Select(u => u.SyntaxTree).ToImmutableArray();
                SetAnalyzerContext(context);
                var compilation = new CompilationWithAnalyzers(request.Compilation, analyzers.CastArray<DiagnosticAnalyzer>(), options);
                var diagnostics = await compilation.GetAllDiagnosticsAsync();
                response.Diagnostics = ConvertDiagnostics(diagnostics);
            }
            catch (Exception e)
            {
                var message = string.Format(DiagnosticResources.ValidatorExceptionMessage, e.GetType().Name);
                response.Diagnostics = new ImmutableArray<ValidationDiagnostic>
                {
                    new ValidationDiagnostic(DiagnosticIds.ValidatorException, message, ValidationDiagnosticLocation.None)
                };
            }
            return response;
        }

        private void ClearAnalyzerContext()
        {
            foreach (var analyzer in analyzers)
            {
                analyzer.ValidationAnalyzerContext = null;
            }
        }

        private ImmutableArray<ValidationDiagnostic> ConvertDiagnostics(ImmutableArray<Diagnostic> diagnostics)
        {
            var validationDiagnostics = new List<ValidationDiagnostic>();
            for (int i = 0; i < diagnostics.Length; i++)
            {
                var diagnostic = diagnostics[i];
                var location = diagnostic.Location.Kind == LocationKind.None ? ValidationDiagnosticLocation.None : new ValidationDiagnosticLocation(diagnostic.Location.SourceSpan.Start, diagnostic.Location.SourceSpan.End);
                var validationDiagnostic = new ValidationDiagnostic(diagnostic.Id, diagnostic.GetMessage(), location);
                validationDiagnostics.Add(validationDiagnostic);
            }
            return validationDiagnostics.ToImmutableArray();
        }

        private void SetAnalyzerContext(CSharpStaticAnalysisContext context)
        {
            foreach (var analyzer in analyzers)
            {
                analyzer.ValidationAnalyzerContext = context;
            }
        }
    }
}