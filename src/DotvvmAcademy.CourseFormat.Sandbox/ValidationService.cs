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
            services.AddScoped<Context>();
            services.AddTransient(p => p.GetService<Context>().Converter);
            services.AddTransient(p => p.GetService<Context>().SourceCodeStorage);
            services.AddTransient(p => p.GetService<Context>().Compilation);
            services.AddTransient(p => p.GetService<Context>().Tree);
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

            var experimentalType = typeof(DotvvmExperimentalFeaturesConfiguration);
            this.services.GetRequiredService<DotvvmConfiguration>()
                .ExperimentalFeatures.ExplicitAssemblyLoading.Enable();
        }

        public async Task<IEnumerable<IValidationDiagnostic>> Validate(
            IEnumerable<IConstraint> constraints,
            IEnumerable<ISourceCode> sources)
        {
            // Admittedly, this method is very very long. It is, however, good enough for the time being.

            using var scope = services.CreateScope();
            // create a unique name for this validation
            var id = Guid.NewGuid();

            // initialize a storage of source codes available from anywhere
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.SourceCodeStorage = new SourceCodeStorage(sources);

            // create the reporter and its helper function
            var reporter = scope.ServiceProvider.GetRequiredService<IValidationReporter>();
            IEnumerable<IValidationDiagnostic> Report()
            {
                return reporter.GetDiagnostics()
                    .Where(d => d.Source == null || d.Source.IsValidated);
            }

            // create a compilation
            var trees = sources.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(
                    text: s.GetContent(),
                    path: s.FileName));
            context.Compilation = CSharpCompilation.Create(
                assemblyName: $"ValidatedUserCode.{id}",
                syntaxTrees: trees,
                references: Dependencies.Select(RoslynReference.FromName),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // load dependencies and create the MetaConverter
            var assemblies = Dependencies.Select(Assembly.Load)
                .ToImmutableArray();

            // add the C# compilation as an Assembly as well
            using (var memoryStream = new MemoryStream())
            {
                var emitResult = context.Compilation.Emit(memoryStream);
                if (!emitResult.Success)
                {
                    foreach (var diagnostic in emitResult.Diagnostics)
                    {
                        reporter.Report(diagnostic);
                    }
                    return Report();
                }
                memoryStream.Position = 0;
                var additionalAssembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);
                assemblies = assemblies.Add(additionalAssembly);
                var cache = scope.ServiceProvider.GetRequiredService<CompiledAssemblyCache>();

                // TODO: Add a reasonable way to add an assembly to the cache.
                var cacheField = typeof(CompiledAssemblyCache).GetField(
                    "cachedAssemblies",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                var actualCache = (ConcurrentDictionary<string, Assembly>)cacheField.GetValue(cache);
                actualCache.TryAdd(additionalAssembly.FullName, additionalAssembly);
            }

            context.Converter = new MetaConverter(context.Compilation, assemblies);

            // parse potential dothtml source
            var dothtmlSource = sources.OfType<DothtmlSourceCode>()
                .SingleOrDefault();
            ResolvedTreeRoot resolvedTree = null;
            if (dothtmlSource != null)
            {
                try
                {
                    // parse syntax
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

                    // parse semantics
                    var resolver = scope.ServiceProvider.GetRequiredService<IControlTreeResolver>();
                    resolvedTree = (ResolvedTreeRoot)resolver.ResolveTree(dothtmlRoot, dothtmlSource.FileName);
                    resolvedTree.FileName = dothtmlSource.FileName;
                    var visitor = new ErrorAggregatingVisitor(reporter);
                    resolvedTree.Accept(visitor);
                }
                catch (DotvvmCompilationException exception)
                {
                    reporter.Report(exception, dothtmlSource);
                }

                // cancel validation if validation couldn't be created
                if (resolvedTree == null)
                {
                    return Report();
                }

                // wrap the validation tree in the XPath tree
                var xpathVisitor = scope.ServiceProvider.GetRequiredService<XPathTreeVisitor>();
                context.Tree = xpathVisitor.Visit(resolvedTree);
            }

            // generate static errors and prepare data for other validation steps
            foreach (var constraint in constraints)
            {
                constraint.Validate(scope.ServiceProvider);
            }

            // run roslyn analyzers concurrently and collect diagnostics
            var analyzers = scope.ServiceProvider.GetRequiredService<IEnumerable<DiagnosticAnalyzer>>()
                .ToImmutableArray();
            var analysisOptions = new CompilationWithAnalyzersOptions(
                options: null,
                onAnalyzerException: (e, a, d) => reporter.Report(d),
                concurrentAnalysis: true,
                logAnalyzerExecutionTime: false,
                reportSuppressedDiagnostics: true);
            var compilationWithAnalyzers = context.Compilation.WithAnalyzers(analyzers, analysisOptions);
            var roslynDiagnostics = await compilationWithAnalyzers.GetAllDiagnosticsAsync();
            foreach (var roslynDiagnostic in roslynDiagnostics)
            {
                reporter.Report(roslynDiagnostic);
            }

            // DO NOT proceed to dynamic analysis if there are errors
            var hasErrors = reporter.GetDiagnostics()
                .Any(d => d.Severity == ValidationSeverity.Error && (d.Source?.IsValidated ?? true));
            if (hasErrors)
            {
                return Report();
            }

            // load user's code
            Assembly userCodeAssembly;
            using (var memoryStream = new MemoryStream())
            {
                var result = context.Compilation.Emit(memoryStream);
                if (!result.Success)
                {
                    throw new InvalidOperationException("Emitting failed despite there being no diagnostics.");
                }
                memoryStream.Position = 0;
                userCodeAssembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);
            }
            assemblies = assemblies.Add(userCodeAssembly);
            context.Converter = new MetaConverter(context.Compilation, assemblies);

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

            // finally return whatever diagnostics were found
            return Report();
        }

        private class Context
        {
            public Compilation Compilation { get; set; }

            public MetaConverter Converter { get; set; }

            public SourceCodeStorage SourceCodeStorage { get; set; }

            public XPathDothtmlRoot Tree { get; set; }
        }
    }
}
