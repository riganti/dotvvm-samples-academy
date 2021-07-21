#nullable enable

using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DotvvmAcademy.CourseFormat.Sandbox
{
    public class ValidationService : IValidationService
    {
        public readonly string[] Dependencies = new string[]
        {
            "mscorlib",
            "netstandard",
            "System.Private.CoreLib",
            "System.Runtime",
            "System.Collections",
            "System.Reflection",
            "System.Linq",
            "System.Linq.Expressions",
            "System.ComponentModel.Annotations",
            "DotVVM.Framework",
            "DotVVM.Core"
        };

        private readonly IServiceProvider services;

        public ValidationService()
        {
            var services = new ServiceCollection();
            DotvvmServiceCollectionExtensions.RegisterDotVVMServices(services);
            services.AddSingleton<IViewModelProtector, FakeViewModelProtector>();
            services.AddScoped<ValidationRun>();
            services.AddTransient(p => p.GetRequiredService<ValidationRun>().Converter!);
            services.AddTransient(p => p.GetRequiredService<ValidationRun>().SourceCodeStorage);
            services.AddTransient(p => p.GetRequiredService<ValidationRun>().Tree!);
            services.AddTransient(p => p.GetRequiredService<ValidationRun>().Compilation!);
            services.AddScoped<AllowedSymbolStorage>();
            services.AddScoped<DynamicActionStorage>();
            services.AddScoped<DiagnosticAnalyzer, AllowedSymbolAnalyzer>();
            services.AddScoped<IValidationReporter, ValidationReporter>();
            services.AddScoped<CSharpDynamicContext>();
            services.AddScoped<IAttributeExtractor, AttributeExtractor>();
            services.AddScoped<ITypedConstantExtractor, TypedConstantExtractor>();
            services.AddScoped<NodeLocator>();
            services.AddScoped<XPathTreeVisitor>();
            services.AddScoped<XPathDothtmlNamespaceResolver>();
            services.AddScoped<NameTable>();
            this.services = services.BuildServiceProvider();

            // this allows the CompiledAssemblyHack down below
            var experimentalType = typeof(DotvvmExperimentalFeaturesConfiguration);
            this.services.GetRequiredService<DotvvmConfiguration>()
                .ExperimentalFeatures.ExplicitAssemblyLoading.Enable();
        }

        public async Task<IEnumerable<IValidationDiagnostic>> Validate(
            IEnumerable<IConstraint> constraints,
            IEnumerable<ISourceCode> sources)
        {
            using var scope = services.CreateScope();
            var run = scope.ServiceProvider.GetRequiredService<ValidationRun>();
            run.Services = scope.ServiceProvider;
            run.SourceCodeStorage = new SourceCodeStorage(sources);
            var reporter = scope.ServiceProvider.GetRequiredService<IValidationReporter>();

            // compile C# sources
            {
                var result = await CompileCSharpSources(run);
                run.Compilation = result.compilation;
                if (result.assembly is null)
                {
                    // compilation failed, so don't bother validating further
                    return reporter.GetDiagnostics();
                }

                var assemblies = Dependencies.Select(Assembly.Load)
                    .Append(result.assembly)
                    .ToImmutableArray();
                run.Converter = new MetaConverter(result.compilation, assemblies);
            }

            // compile the dothtml source (if there is one)
            run.Tree = CompileDothtmlSource(run);
            if (run.Tree is null)
            {
                return reporter.GetDiagnostics();
            }

            // check static constraints
            foreach (var constraint in constraints)
            {
                constraint.Validate(scope.ServiceProvider);
            }

            // DO NOT proceed to dynamic analysis if there are errors
            var hasErrors = reporter.GetDiagnostics().Any(d => d.Severity == ValidationSeverity.Error);
            if (hasErrors)
            {
                return reporter.GetDiagnostics();
            }

            // run dynamic analysis
            var dynamicContext = scope.ServiceProvider.GetRequiredService<CSharpDynamicContext>();
            var dynamicActionStorage = scope.ServiceProvider.GetRequiredService<DynamicActionStorage>();
            foreach (var action in dynamicActionStorage.DynamicActions)
            {
                try
                {
                    action(dynamicContext);
                }
                catch (Exception exception)
                {
                    reporter.Report($"An '{exception.GetType().Name}' with message: '{exception.Message}', " +
                        $"occured during execution of your code");
                }
            }

            return reporter.GetDiagnostics();
        }

        private async Task<(Compilation compilation, Assembly? assembly)> CompileCSharpSources(ValidationRun run)
        {
            var reporter = run.Services.GetRequiredService<IValidationReporter>();

            var trees = run.SourceCodeStorage.Sources.Values.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(
                    text: s.GetContent(),
                    path: s.FileName));
            var compilation = CSharpCompilation.Create(
                assemblyName: $"ValidatedUserCode.{run.Id}",
                syntaxTrees: trees,
                references: Dependencies.Select(RoslynReference.FromName),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // run roslyn analyzers concurrently and collect diagnostics
            var analyzers = run.Services.GetRequiredService<IEnumerable<DiagnosticAnalyzer>>()
                .ToImmutableArray();
            var analysisOptions = new CompilationWithAnalyzersOptions(
                options: new AnalyzerOptions(default),
                onAnalyzerException: (e, a, d) => reporter.Report(d),
                concurrentAnalysis: true,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers, analysisOptions);
            var roslynDiagnostics = await compilationWithAnalyzers.GetAllDiagnosticsAsync();
            foreach (var roslynDiagnostic in roslynDiagnostics)
            {
                reporter.Report(roslynDiagnostic);
            }

            // add the C# compilation as an Assembly as well
            using var memoryStream = new MemoryStream();
            var emitResult = compilation.Emit(memoryStream);
            if (!emitResult.Success)
            {
                return (compilation, null);
            }
            memoryStream.Position = 0;
            var additionalAssembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);
            var cache = run.Services.GetRequiredService<CompiledAssemblyCache>();

            // TODO: Add a reasonable way to add an assembly to the cache.
            var cacheField = typeof(CompiledAssemblyCache).GetField(
                "cachedAssemblies",
                BindingFlags.Instance | BindingFlags.NonPublic)!;
            var actualCache = (ConcurrentDictionary<string, Assembly>)cacheField.GetValue(cache)!;
            actualCache.TryAdd(additionalAssembly.FullName!, additionalAssembly);
            return (compilation, additionalAssembly);
        }

        private XPathDothtmlRoot? CompileDothtmlSource(ValidationRun run)
        {
            var dothtmlSource = run.SourceCodeStorage!.Sources.Values.OfType<DothtmlSourceCode>().SingleOrDefault();
            if (dothtmlSource is null)
            {
                return null;
            }

            var reporter = run.Services.GetRequiredService<IValidationReporter>();

            try
            {
                // TODO: Figure out how to handle master pages.
                var tokenizer = new DothtmlTokenizer();
                tokenizer.Tokenize(dothtmlSource.GetContent() ?? string.Empty);
                foreach (var token in tokenizer.Tokens)
                {
                    if (token.HasError)
                    {
                        reporter.Report(token.Error, dothtmlSource);
                    }
                }
                var parser = new DothtmlParser();
                var dothtmlRoot = parser.Parse(tokenizer.Tokens);

                var resolver = run.Services.GetRequiredService<IControlTreeResolver>();
                var resolvedTree = (ResolvedTreeRoot)resolver.ResolveTree(dothtmlRoot, dothtmlSource.FileName);
                resolvedTree.FileName = dothtmlSource.FileName;
                var visitor = new ErrorAggregatingVisitor(reporter);
                resolvedTree.Accept(visitor);

                var xpathVisitor = run.Services.GetRequiredService<XPathTreeVisitor>();
                return xpathVisitor.Visit(resolvedTree);
            }
            catch (DotvvmCompilationException exception)
            {
                reporter.Report(exception, dothtmlSource);
            }

            return null;
        }

        private class ValidationRun
        {
            public Guid Id { get; } = Guid.NewGuid();

            public IServiceProvider Services { get; set; }
                = new ServiceCollection().BuildServiceProvider();

            public SourceCodeStorage SourceCodeStorage { get; set; }
                = new SourceCodeStorage(Enumerable.Empty<ISourceCode>());

            public MetaConverter? Converter { get; set; }

            public Compilation? Compilation { get;set; }

            public XPathDothtmlRoot? Tree { get; set; }
        }
    }
}
