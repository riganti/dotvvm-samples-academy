using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class StaticAnalysisMiddleware : IValidationMiddleware
    {
        public const string CompilationWithAnalyzersOptionsKey = "CompilationWithAnalyzersOptions";
        public const string CSharpCompilationKey = "CSharpCompilation";
        public const string MetadataCollectionKey = "MetadataCollection";

        public async Task InvokeAsync(ValidationContext context, ValidationDelegate next)
        {
            var compilation = context.GetRequiredItem<CSharpCompilation>(CSharpCompilationKey);
            var cwa = new CompilationWithAnalyzers(
                compilation: compilation,
                analyzers: GetAnalyzers(context),
                analysisOptions: GetOptions(context));
            var diagnostics = await cwa.GetAllDiagnosticsAsync();
            foreach(var diagnostic in diagnostics)
            {
                context.ReportDiagnostic(new RoslynValidationDiagnostic(diagnostic));
            }
            await next(context);
        }

        private CompilationWithAnalyzersOptions GetOptions(ValidationContext context)
        {
            return context.GetItem<CompilationWithAnalyzersOptions>(CompilationWithAnalyzersOptionsKey)
                ?? new CompilationWithAnalyzersOptions(
                    options: null,
                    onAnalyzerException: null,
                    concurrentAnalysis: false,
                    logAnalyzerExecutionTime: false);
        }

        private ImmutableArray<DiagnosticAnalyzer> GetAnalyzers(ValidationContext context)
        {
            return context
                .GetRequiredItem<IServiceProvider>(MiddlewareValidationService.ServicesKey)
                .GetRequiredService<IEnumerable<DiagnosticAnalyzer>>()
                .ToImmutableArray();
        }
    }
}