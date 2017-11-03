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
        private readonly ImmutableArray<DiagnosticAnalyzer> Analyzers = ImmutableArray.Create((DiagnosticAnalyzer)new RequiredSymbolAnalyzer());
        private readonly ImmutableDictionary<string, CSharpValidationMethod> methods;
        private readonly IServiceProvider serviceProvider;
        private CompilationWithAnalyzersOptions options = new CompilationWithAnalyzersOptions(null, null, false, false);

        protected internal DefaultCSharpValidator(ImmutableDictionary<string, CSharpValidationMethod> methods, IServiceProvider serviceProvider)
        {
            this.methods = methods ?? throw new ArgumentNullException(nameof(methods));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<CSharpValidationResponse> Validate(CSharpValidationRequest request)
        {
            var response = new CSharpValidationResponse();
            ClearAnalyzerContext();
            try
            {
                var validatedTrees = request.ValidationUnits.Select(u => u.SyntaxTree).ToImmutableArray();
                var method = request.ValidationUnits
                    .SelectMany(u => u.ValidationMethods)
                    .Select(m => methods[m])
                    .Aggregate((first, second) => first.Merge(second));
                var context = new CSharpValidationContext(request, response, method, validatedTrees);
                SetAnalyzerContext(context);
                var compilation = new CompilationWithAnalyzers(request.Compilation, Analyzers, options);
                var diagnostics = await compilation.GetAllDiagnosticsAsync();
                response.Diagnostics = ConvertDiagnostics(diagnostics);
            }
            catch (Exception e)
            {
                var message = string.Format(DiagnosticResources.ValidatorExceptionMessage, e.GetType().Name);
                response.Diagnostics = new ImmutableArray<ValidationDiagnostic>
                {
                    new ValidationDiagnostic(ValidationDiagnosticIds.ValidatorException, message, ValidationDiagnosticLocation.None)
                };
            }
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
                var location = diagnostic.Location.Kind == LocationKind.None ? ValidationDiagnosticLocation.None : new ValidationDiagnosticLocation(diagnostic.Location.SourceSpan.Start, diagnostic.Location.SourceSpan.End);
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