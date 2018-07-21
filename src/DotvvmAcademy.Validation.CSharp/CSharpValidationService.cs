using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp.Unit;
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
    public class CSharpValidationService : IValidationService<CSharpUnit, CSharpValidationOptions>
    {
        private readonly IServiceProvider globalProvider;

        public CSharpValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public async Task<ImmutableArray<IValidationDiagnostic>> Validate(
            CSharpUnit unit,
            string code,
            CSharpValidationOptions options = null)
        {
            options = options ?? CSharpValidationOptions.Default;
            using (var scope = globalProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                var reporter = scope.ServiceProvider.GetRequiredService<ValidationReporter>();
                context.Unit = unit;
                context.Code = code;
                context.Options = options;
                await RunAnalyzers(scope.ServiceProvider);
                if (reporter.Count > 0)
                {
                    return reporter.GetDiagnostics();
                }

                HandleQueries<ITypeSymbol>(scope.ServiceProvider);
                HandleQueries<IMethodSymbol>(scope.ServiceProvider);
                HandleQueries<IPropertySymbol>(scope.ServiceProvider);
                HandleQueries<IFieldSymbol>(scope.ServiceProvider);
                HandleQueries<IEventSymbol>(scope.ServiceProvider);
                if (reporter.Count > 0)
                {
                    return reporter.GetDiagnostics();
                }

                context.Assembly = await GetAssembly(scope.ServiceProvider);
                RunDynamicActions(scope.ServiceProvider);
                return reporter.GetDiagnostics();
            }
        }

        private async Task<Assembly> GetAssembly(IServiceProvider provider)
        {
            var compilation = provider.GetRequiredService<CSharpCompilation>();
            using (var originalStream = new MemoryStream())
            using (var rewrittenStream = new MemoryStream())
            {
                var result = compilation.Emit(originalStream);
                if (!result.Success)
                {
                    var reporter = provider.GetRequiredService<ValidationReporter>();
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        reporter.Report(diagnostic);
                    }
                    return null;
                }

                originalStream.Position = 0;
                var rewriter = provider.GetRequiredService<IAssemblyRewriter>();
                await rewriter.Rewrite(originalStream, rewrittenStream);
                rewrittenStream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            }
        }

        private CSharpCompilation GetCompilation(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var tree = CSharpSyntaxTree.ParseText(context.Code);
            var compilation = CSharpCompilation.Create(
                assemblyName: $"DotvvmAcademy.Validation.CSharp.{context.Id}",
                syntaxTrees: new[] { tree },
                references: new[]
                {
                    MetadataReferencer.FromName("mscorlib"),
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("System.Collections"),
                    MetadataReferencer.FromName("System.Reflection")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            if (context.Options.IncludeCompilerDiagnostics)
            {
                var reporter = provider.GetRequiredService<ValidationReporter>();
                var diagnostics = compilation.GetDiagnostics();
                foreach (var diagnostic in diagnostics)
                {
                    reporter.Report(diagnostic);
                }
            }
            return compilation;
        }

        private ImmutableArray<ISymbol> GetMetadataNameResult(IServiceProvider provider, MetadataName name)
        {
            var locator = provider.GetRequiredService<SymbolLocator>();
            if (locator.TryLocate(name, out var symbol))
            {
                return ImmutableArray.Create(symbol);
            }

            return default;
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddSingleton<IMetadataNameFactory, MetadataNameFactory>();
            c.AddSingleton<MetadataNameParser>();
            c.AddSingleton<RoslynMetadataNameProvider>();
            c.AddSingleton<ReflectionMetadataNameProvider>();
            c.AddSingleton<IAssemblyRewriter, AssemblyRewriter>();
            c.AddScoped<AllowedSymbolStorage>();
            c.AddScoped<DiagnosticAnalyzer, SymbolAllowedAnalyzer>(p =>
            {
                var allower = p.GetRequiredService<AllowedSymbolStorage>();
                return new SymbolAllowedAnalyzer(allower.Builder.ToImmutable());
            });
            c.AddScoped(GetCompilation);
            c.AddScoped<ValidationReporter>();
            c.AddScoped<Context>();
            c.AddScoped<SymbolLocator>();
            c.AddScoped(p => p.GetRequiredService<Context>().Assembly);
            c.AddScoped<MemberInfoLocator>();
            c.AddScoped<CSharpDynamicContext>();
            return c.BuildServiceProvider();
        }

        private void HandleQueries<TResult>(IServiceProvider provider)
            where TResult : ISymbol
        {
            var unit = provider.GetRequiredService<Context>().Unit;
            var queries = unit.Queries.Values.OfType<CSharpQuery<TResult>>();
            foreach (var query in queries)
            {
                var result = GetMetadataNameResult(provider, query.Name).Cast<TResult>().ToImmutableArray();
                foreach (var constraint in query.Constraints)
                {
                    var context = new CSharpConstraintContext<TResult>(provider, query.Name, result);
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
            var reporter = provider.GetRequiredService<ValidationReporter>();
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
                concurrentAnalysis: true,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilation = new CompilationWithAnalyzers(
                compilation: provider.GetRequiredService<CSharpCompilation>(),
                analyzers: analyzers,
                analysisOptions: analysisOptions);
            var diagnostics = await compilation.GetAnalyzerDiagnosticsAsync();
            var reporter = provider.GetRequiredService<ValidationReporter>();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
        }

        private void RunDynamicActions(IServiceProvider provider)
        {
            var unit = provider.GetRequiredService<Context>().Unit;
            var context = provider.GetRequiredService<CSharpDynamicContext>();
            var reporter = provider.GetRequiredService<ValidationReporter>();
            foreach (var action in unit.DynamicActions)
            {
                try
                {
                    action(context);
                }
                catch (Exception e)
                {
                    reporter.Report(e.Message);
                }
            }
        }

        private class Context
        {
            public Assembly Assembly { get; set; }

            public string Code { get; set; }

            public Guid Id { get; } = Guid.NewGuid();

            public CSharpValidationOptions Options { get; set; }

            public CSharpUnit Unit { get; set; }
        }
    }
}