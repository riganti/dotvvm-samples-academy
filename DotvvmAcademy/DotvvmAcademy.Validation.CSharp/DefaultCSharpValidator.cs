using DotvvmAcademy.Validation.CSharp.Abstractions;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidator : ICSharpValidator
    {
        private readonly CompilationWithAnalyzersOptions options = new CompilationWithAnalyzersOptions(null, null, true, false);
        private readonly IServiceProvider provider;

        protected internal DefaultCSharpValidator(IServiceProvider provider)
        {
            this.provider = provider;
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
            var analyzers = provider.GetRequiredService<IEnumerable<ValidationAnalyzer>>().ToImmutableArray();
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
                diagnostics.Add(new ValidationDiagnostic(
                    id: roslynDiagnostic.Id,
                    message: roslynDiagnostic.GetMessage(),
                    location: GetLocation(request, roslynDiagnostic)));
            }
            response.Diagnostics = diagnostics.ToImmutable();
        }

        private ValidationDiagnosticLocation GetLocation(CSharpValidationRequest request, Diagnostic roslynDiagnostic)
        {
            if (roslynDiagnostic.Location.Kind == LocationKind.None)
            {
                return ValidationDiagnosticLocation.None;
            }

            var fileKey = request.FileTable
                .SingleOrDefault(pair => pair.Value == roslynDiagnostic.Location.SourceTree)
                .Key;
            return new ValidationDiagnosticLocation(
                start: roslynDiagnostic.Location.SourceSpan.Start,
                end: roslynDiagnostic.Location.SourceSpan.End,
                fileKey: fileKey);
        }
    }
}