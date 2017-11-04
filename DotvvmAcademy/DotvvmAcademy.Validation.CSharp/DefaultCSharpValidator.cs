using DotvvmAcademy.Validation.CSharp.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidator : ICSharpValidator
    {
        private readonly ImmutableArray<ValidationAnalyzer> analyzers;
        private readonly IServiceProvider serviceProvider;
        private CompilationWithAnalyzersOptions options = new CompilationWithAnalyzersOptions(null, null, false, false);

        protected internal DefaultCSharpValidator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            analyzers = serviceProvider.GetRequiredService<IEnumerable<ValidationAnalyzer>>().ToImmutableArray();
        }

        public async Task<CSharpValidationResponse> Validate(CSharpValidationRequest request)
        {
            var response = new CSharpValidationResponse();
            if ((request.ValidationExtent & CSharpValidationExtent.StaticAnalysis) != CSharpValidationExtent.None)
            {
                await RunStaticAnalysis(request, response);
            }
            if (response.Diagnostics.Length != 0)
            {
                return response;
            }
            if ((request.ValidationExtent & CSharpValidationExtent.AssemblyRewrite) != CSharpValidationExtent.None)
            {
                await RewriteAssembly(request, response);
            }
            if (response.Diagnostics.Length != 0)
            {
                return response;
            }
            if ((request.ValidationExtent & CSharpValidationExtent.DynamicAnalysis) != CSharpValidationExtent.None)
            {
                await RunDynamicAnalysis(request, response);
            }
            return response;
        }

        protected virtual async Task RewriteAssembly(CSharpValidationRequest request, CSharpValidationResponse response)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task RunDynamicAnalysis(CSharpValidationRequest request, CSharpValidationResponse response)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task RunStaticAnalysis(CSharpValidationRequest request, CSharpValidationResponse response)
        {
            foreach (var analyzer in analyzers)
            {
                analyzer.StaticAnalysis = request.StaticAnalysis;
            }
            var compilation = new CompilationWithAnalyzers(
                compilation: request.Compilation,
                analyzers: analyzers.CastArray<DiagnosticAnalyzer>(),
                analysisOptions: options);
            var roslynDiagnostics = await compilation.GetAllDiagnosticsAsync();
            var diagnostics = ImmutableArray.CreateBuilder<ValidationDiagnostic>();
            foreach (var roslynDiagnostic in roslynDiagnostics)
            {
                var location = roslynDiagnostic.Location.Kind == LocationKind.None
                    ? ValidationDiagnosticLocation.None
                    : new ValidationDiagnosticLocation(
                        startPosition: roslynDiagnostic.Location.SourceSpan.Start,
                        endPosition: roslynDiagnostic.Location.SourceSpan.End);
                diagnostics.Add(new ValidationDiagnostic(
                    id: roslynDiagnostic.Id,
                    message: roslynDiagnostic.GetMessage(),
                    location: location));
            }
            response.Diagnostics = diagnostics.ToImmutable();
        }
    }
}