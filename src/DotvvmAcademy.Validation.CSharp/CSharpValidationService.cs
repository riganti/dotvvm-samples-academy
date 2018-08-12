using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationService : IValidationService<CSharpUnit>
    {
        private readonly IServiceProvider globalProvider;

        public CSharpValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public async Task<ImmutableArray<IValidationDiagnostic>> Validate(
            CSharpUnit unit,
            ImmutableArray<ISourceCode> sources)
        {
            using (var scope = globalProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                var compilationAccessor = scope.ServiceProvider.GetRequiredService<ICSharpCompilationAccessor>();
                compilationAccessor.Compilation = GetCompilation(scope.ServiceProvider);
                var reporter = scope.ServiceProvider.GetRequiredService<CSharpValidationReporter>();
                context.Unit = unit;
                context.Sources = sources;
                HandleQueries<ITypeSymbol>(scope.ServiceProvider);
                HandleQueries<IMethodSymbol>(scope.ServiceProvider);
                HandleQueries<IPropertySymbol>(scope.ServiceProvider);
                HandleQueries<IFieldSymbol>(scope.ServiceProvider);
                HandleQueries<IEventSymbol>(scope.ServiceProvider);
                await RunAnalyzers(scope.ServiceProvider);
                if (reporter.WorstSeverity == ValidationSeverity.Error)
                {
                    return reporter.GetReportedDiagnostics().ToImmutableArray();
                }

                var assemblyAccessor = scope.ServiceProvider.GetRequiredService<IAssemblyAccessor>();
                var userAssembly = await GetAssembly(scope.ServiceProvider);
                var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
                var builder = ImmutableArray.CreateBuilder<Assembly>();
                builder.Add(userAssembly);
                builder.AddRange(assemblies);
                assemblyAccessor.Assemblies = builder.ToImmutable();
                RunDynamicActions(scope.ServiceProvider);
                return reporter.GetReportedDiagnostics().ToImmutableArray();
            }
        }

        private async Task<Assembly> GetAssembly(IServiceProvider provider)
        {
            var compilation = provider.GetRequiredService<ICSharpCompilationAccessor>().Compilation;
            using (var originalStream = new MemoryStream())
            using (var rewrittenStream = new MemoryStream())
            {
                var result = compilation.Emit(originalStream);
                if (!result.Success)
                {
                    var reporter = provider.GetRequiredService<CSharpValidationReporter>();
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        reporter.Report(diagnostic);
                    }
                    return null;
                }

                originalStream.Position = 0;
                var rewriter = provider.GetRequiredService<AssemblyRewriter>();
                await rewriter.Rewrite(originalStream, rewrittenStream);
                rewrittenStream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            }
        }

        private CSharpCompilation GetCompilation(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var trees = context.Sources
                .OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(
                    text: s.GetContent(),
                    path: s.FileName));
            var compilation = CSharpCompilation.Create(
                assemblyName: $"DotvvmAcademy.Validation.CSharp.{context.Id}",
                syntaxTrees: trees,
                references: new[]
                {
                    RoslynReference.FromName("mscorlib"),
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var reporter = provider.GetRequiredService<CSharpValidationReporter>();
            var diagnostics = compilation.GetDiagnostics();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
            return compilation;
        }

        private ImmutableArray<ISymbol> GetMetadataNameResult(IServiceProvider provider, string name)
        {
            return provider.GetRequiredService<ISymbolLocator>().Locate(name);
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddMetaSingletonFriendly();
            c.AddScoped<Context>();
            c.AddSingleton<AssemblyRewriter>();
            c.AddScoped<AllowedSymbolStorage>();
            c.AddScoped<DiagnosticAnalyzer, SymbolAllowedAnalyzer>();
            c.AddScoped<IValidationReporter>(p => p.GetRequiredService<CSharpValidationReporter>());
            c.AddScoped<CSharpValidationReporter>();
            c.AddScoped(p => 
            {
                var context = p.GetRequiredService<Context>();
                return new CSharpSourceCodeProvider(context.Sources.OfType<CSharpSourceCode>());
            });
            c.AddScoped<CSharpDynamicContext>();
            return c.BuildServiceProvider();
        }

        private void HandleQueries<TResult>(IServiceProvider provider)
            where TResult : ISymbol
        {
            var unit = provider.GetRequiredService<Context>().Unit;
            foreach (var query in unit.GetQueries<TResult>())
            {
                var result = GetMetadataNameResult(provider, query.Source).OfType<TResult>().ToImmutableArray();
                foreach (var constraint in query.GetConstraints())
                {
                    var context = new ConstraintContext<TResult>(provider, query, result);
                    constraint(context);
                }
            }
        }

        private void OnAnalyzerException(
            IServiceProvider provider,
            Exception exception,
            DiagnosticAnalyzer analyzer,
            Diagnostic diagnostic)
        {
            var reporter = provider.GetRequiredService<CSharpValidationReporter>();
            reporter.Report(diagnostic);
        }

        private async Task RunAnalyzers(IServiceProvider provider)
        {
            var analyzers = provider
                .GetRequiredService<IEnumerable<DiagnosticAnalyzer>>()
                .ToImmutableArray();
            var analysisOptions = new CompilationWithAnalyzersOptions(
                options: null,
                onAnalyzerException: (e, a, d) => OnAnalyzerException(provider, e, a, d),
                concurrentAnalysis: false,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilation = provider.GetRequiredService<ICSharpCompilationAccessor>().Compilation
                .WithAnalyzers(analyzers, analysisOptions);
            var diagnostics = await compilation.GetAnalyzerDiagnosticsAsync();
            var reporter = provider.GetRequiredService<CSharpValidationReporter>();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
        }

        private void RunDynamicActions(IServiceProvider provider)
        {
            var unit = provider.GetRequiredService<Context>().Unit;
            var context = provider.GetRequiredService<CSharpDynamicContext>();
            var reporter = provider.GetRequiredService<CSharpValidationReporter>();
            foreach (var action in unit.GetDynamicActions())
            {
                try
                {
                    action(context);
                }
                catch (Exception exception)
                {
                    reporter.Report($"An '{exception.GetType().Name}' with message: '{exception.Message}', " +
                        $"occured during execution of your code");
                }
            }
        }

        private class Context
        {
            public Guid Id { get; } = Guid.NewGuid();

            public ImmutableArray<ISourceCode> Sources { get; set; }

            public CSharpUnit Unit { get; set; }
        }
    }
}