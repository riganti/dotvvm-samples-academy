using DotvvmAcademy.Meta;
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
    public class CSharpValidationService : IValidationService
    {
        private readonly IServiceProvider globalProvider;

        public CSharpValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public async Task<IEnumerable<IValidationDiagnostic>> Validate(IEnumerable<IConstraint> constraints, IEnumerable<ISourceCode> sources)
        {
            if (sources.Any(s => !(s is CSharpSourceCode)))
            {
                throw new ArgumentException("Only CSharpSourceCode objects are supported.", nameof(sources));
            }

            using (var scope = globalProvider.CreateScope())
            {
                var id = Guid.NewGuid();

                // compile user code and prepare MetaConverter
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                context.SourceCodeStorage = new SourceCodeStorage(sources);
                var reporter = scope.ServiceProvider.GetRequiredService<IValidationReporter>();
                var compilation = GetCompilation(reporter, sources, id);
                var assemblies = Assembly.GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .ToImmutableArray();
                context.Converter = new MetaConverter(compilation, assemblies);

                // constraints, static analysis
                foreach (var constraint in constraints)
                {
                    constraint.Validate(scope.ServiceProvider);
                }
                var analyzers = scope.ServiceProvider.GetRequiredService<IEnumerable<DiagnosticAnalyzer>>();
                await RunAnalyzers(reporter, compilation, analyzers);
                if (reporter.GetDiagnostics()
                    .Any(d => d.Source.IsValidated && d.Severity == ValidationSeverity.Error))
                {
                    return GetValidationDiagnostics(reporter);
                }

                // dynamic analysis
                var userAssembly = GetAssembly(reporter, compilation);
                assemblies = assemblies.Add(userAssembly);
                context.Converter = new MetaConverter(compilation, assemblies);
                var dynamicContext = scope.ServiceProvider.GetRequiredService<CSharpDynamicContext>();
                var dynamicActionStorage = scope.ServiceProvider.GetRequiredService<DynamicActionStorage>();
                RunDynamicActions(reporter, dynamicContext, dynamicActionStorage);
                return GetValidationDiagnostics(reporter);
            }
        }

        private Assembly GetAssembly(IValidationReporter reporter, Compilation compilation)
        {
            using (var originalStream = new MemoryStream())
            using (var rewrittenStream = new MemoryStream())
            {
                var result = compilation.Emit(originalStream);
                if (!result.Success)
                {
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        reporter.Report(diagnostic);
                    }
                    return null;
                }

                originalStream.Position = 0;
                var rewriter = new AssemblyRewriter();
                rewriter.Rewrite(originalStream, rewrittenStream);
                rewrittenStream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            }
        }

        private CSharpCompilation GetCompilation(IValidationReporter reporter, IEnumerable<ISourceCode> sources, Guid id)
        {
            var trees = sources.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(
                    text: s.GetContent(),
                    path: s.FileName));
            var compilation = CSharpCompilation.Create(
                assemblyName: $"ValidatedUserCode.CSharp.{id}",
                syntaxTrees: trees,
                references: new[]
                {
                    RoslynReference.FromName("mscorlib"),
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.Linq"),
                    RoslynReference.FromName("System.Linq.Expressions"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var diagnostics = compilation.GetDiagnostics();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
            return compilation;
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddScoped<Context>();
            c.AddTransient(p => p.GetService<Context>().Converter);
            c.AddTransient(p => p.GetService<Context>().SourceCodeStorage);
            c.AddSingleton<AssemblyRewriter>();
            c.AddScoped<AllowedSymbolStorage>();
            c.AddScoped<DynamicActionStorage>();
            c.AddScoped<DiagnosticAnalyzer, AllowedSymbolAnalyzer>();
            c.AddScoped<IValidationReporter, ValidationReporter>();
            c.AddScoped<CSharpDynamicContext>();
            return c.BuildServiceProvider();
        }

        private ImmutableArray<IValidationDiagnostic> GetValidationDiagnostics(IValidationReporter reporter)
        {
            return reporter.GetDiagnostics()
                .Where(d => d.Source == null || d.Source.IsValidated)
                .ToImmutableArray();
        }

        private async Task RunAnalyzers(IValidationReporter reporter, Compilation compilation, IEnumerable<DiagnosticAnalyzer> analyzers)
        {
            var analysisOptions = new CompilationWithAnalyzersOptions(
                options: null,
                onAnalyzerException: (e, a, d) => reporter.Report(d),
                concurrentAnalysis: false,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilationWithAnalyzers = compilation
                .WithAnalyzers(analyzers.ToImmutableArray(), analysisOptions);
            var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
            foreach (var diagnostic in diagnostics)
            {
                reporter.Report(diagnostic);
            }
        }

        private void RunDynamicActions(IValidationReporter reporter, CSharpDynamicContext context, DynamicActionStorage storage)
        {
            foreach (var action in storage.DynamicActions)
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
            public MetaConverter Converter { get; set; }

            public SourceCodeStorage SourceCodeStorage { get; set; }
        }
    }
}