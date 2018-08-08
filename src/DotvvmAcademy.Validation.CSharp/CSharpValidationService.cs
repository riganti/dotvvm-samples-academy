using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            IOptions<CSharpValidationOptions> options = null)
        {
            options = options ?? new CSharpValidationOptions();
            using (var scope = globalProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                var reporter = scope.ServiceProvider.GetRequiredService<ValidationReporter>();
                context.Unit = unit;
                context.Code = code;
                context.Options = options.Value;
                HandleQueries<ITypeSymbol>(scope.ServiceProvider);
                HandleQueries<IMethodSymbol>(scope.ServiceProvider);
                HandleQueries<IPropertySymbol>(scope.ServiceProvider);
                HandleQueries<IFieldSymbol>(scope.ServiceProvider);
                HandleQueries<IEventSymbol>(scope.ServiceProvider);
                await RunAnalyzers(scope.ServiceProvider);
                var diagnostics = reporter.GetDiagnostics();
                if (diagnostics.Any(d => d.Severity == ValidationSeverity.Error))
                {
                    return diagnostics;
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
                var rewriter = provider.GetRequiredService<AssemblyRewriter>();
                await rewriter.Rewrite(originalStream, rewrittenStream);
                rewrittenStream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            }
        }

        private CSharpCompilation GetCompilation(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var tree = CSharpSyntaxTree.ParseText(context.Code ?? string.Empty);
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

        private ImmutableArray<ISymbol> GetMetadataNameResult(IServiceProvider provider, string name)
        {
            return provider.GetRequiredService<SymbolLocator>().Locate(name);
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddSingleton<AssemblyRewriter>();
            c.AddScoped<AllowedSymbolStorage>();
            c.AddScoped<DiagnosticAnalyzer, SymbolAllowedAnalyzer>();
            c.AddScoped(GetCompilation);
            c.AddScoped<ValidationReporter>();
            c.AddScoped<Context>();
            c.AddScoped<SymbolLocator>();
            c.AddScoped(p => p.GetRequiredService<Context>().Assembly);
            c.AddScoped(p =>
            {
                var userAssembly = p.GetRequiredService<Assembly>();
                var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
                var builder = ImmutableArray.CreateBuilder<Assembly>();
                builder.Add(userAssembly);
                builder.AddRange(assemblies);
                return new MemberInfoLocator(builder.ToImmutable());
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
                concurrentAnalysis: false,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilation = provider.GetRequiredService<CSharpCompilation>()
                .WithAnalyzers(analyzers, analysisOptions);
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
            foreach (var action in unit.GetDynamicActions())
            {
                try
                {
                    action(context);
                }
                catch (Exception e)
                {
                    reporter.Report(e);
                }
            }
        }

        public class Context
        {
            public Assembly Assembly { get; set; }

            public string Code { get; set; }

            public Guid Id { get; } = Guid.NewGuid();

            public CSharpValidationOptions Options { get; set; }

            public CSharpUnit Unit { get; set; }
        }
    }
}