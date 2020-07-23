using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
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
            services = new ServiceCollection()
                .AddScoped<Context>()
                .AddTransient(p => p.GetService<Context>().Converter)
                .AddTransient(p => p.GetService<Context>().SourceCodeStorage)
                .AddTransient(p => p.GetService<Context>().Compilation)
                .AddTransient(p => p.GetService<Context>().Tree)
                .AddScoped<AllowedSymbolStorage>()
                .AddScoped<DynamicActionStorage>()
                .AddScoped<DiagnosticAnalyzer, AllowedSymbolAnalyzer>()
                .AddScoped<IValidationReporter, ValidationReporter>()
                .AddScoped<CSharpDynamicContext>()
                .AddScoped<IAttributeExtractor, AttributeExtractor>()
                .AddScoped<ITypedConstantExtractor, TypedConstantExtractor>()
                .AddScoped<NodeLocator>()
                .AddScoped<ValidationTypeDescriptorFactory>()
                .AddScoped<ValidationControlTypeFactory>()
                .AddScoped<ValidationControlMetadataFactory>()
                .AddScoped<ValidationPropertyFactory>()
                .AddScoped(p =>
                {
                    var controlResolver = ActivatorUtilities.CreateInstance<ValidationControlResolver>(p);
                    controlResolver.RegisterNamespace("dot", "DotVVM.Framework.Controls", "DotVVM.Framework");
                    return controlResolver;
                })
                .AddScoped<ValidationTreeResolver>()
                .AddScoped<ValidationTreeBuilder>()
                .AddScoped<XPathTreeVisitor>()
                .AddScoped<XPathDothtmlNamespaceResolver>()
                .AddScoped<NameTable>()
                .BuildServiceProvider();
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
            context.Converter = new MetaConverter(context.Compilation, assemblies);

            // parse potential dothtml source
            var dothtmlSource = sources.OfType<DothtmlSourceCode>()
                .SingleOrDefault();
            ValidationTreeRoot validationTree = null;
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
                    var resolver = scope.ServiceProvider.GetRequiredService<ValidationTreeResolver>();
                    validationTree = (ValidationTreeRoot)resolver.ResolveTree(dothtmlRoot, dothtmlSource.FileName);
                    validationTree.FileName = dothtmlSource.FileName;
                    var visitor = new ErrorAggregatingVisitor(reporter);
                    foreach (var diagnostic in visitor.Visit(validationTree))
                    {
                        reporter.Report(diagnostic);
                    }
                }
                catch (DotvvmCompilationException exception)
                {
                    reporter.Report(exception, dothtmlSource);
                }

                // cancel validation if validation couldn't be created
                if (validationTree == null)
                {
                    return Report();
                }

                // wrap the validation tree in the XPath tree
                var xpathVisitor = scope.ServiceProvider.GetRequiredService<XPathTreeVisitor>();
                context.Tree = xpathVisitor.Visit(validationTree);
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
