using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationService : IValidationService
    {
        private readonly IServiceProvider globalProvider;

        public DothtmlValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public Task<IEnumerable<IValidationDiagnostic>> Validate(IEnumerable<IConstraint> constraints, IEnumerable<ISourceCode> sources)
        {
            using (var scope = globalProvider.CreateScope())
            {
                var id = Guid.NewGuid();

                // prepare services
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                context.SourceCodeStorage = new SourceCodeStorage(sources);
                var reporter = scope.ServiceProvider.GetRequiredService<IValidationReporter>();
                context.Compilation = GetCompilation(reporter, sources, id);
                var assemblies = Assembly.GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .ToImmutableArray();
                context.Converter = new MetaConverter(context.Compilation, assemblies);

                // compile tree
                var sourceCode = sources.OfType<DothtmlSourceCode>().Single();
                var validationTree = GetValidationTree(
                    reporter,
                    scope.ServiceProvider.GetRequiredService<ValidationTreeResolver>(),
                    scope.ServiceProvider.GetRequiredService<ErrorAggregatingVisitor>(),
                    sourceCode);
                context.Tree = GetXPathTree(scope.ServiceProvider.GetRequiredService<XPathTreeVisitor>(), validationTree);

                foreach (var constraint in constraints)
                {
                    constraint.Validate(scope.ServiceProvider);
                }
                return Task.FromResult<IEnumerable<IValidationDiagnostic>>(GetValidationDiagnostics(reporter));
            }
        }

        private CSharpCompilation GetCompilation(IValidationReporter reporter, IEnumerable<ISourceCode> sources, Guid id)
        {
            var trees = sources.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(s.GetContent()));
            var compilation = CSharpCompilation.Create(
                assemblyName: $"ValidatedUserCode.Dothtml.{id}",
                syntaxTrees: trees,
                references: new[]
                {
                    RoslynReference.FromName("mscorlib"),
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
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
            c.AddTransient(p => p.GetRequiredService<Context>().Converter);
            c.AddTransient(p => p.GetRequiredService<Context>().SourceCodeStorage);
            c.AddTransient(p => p.GetRequiredService<Context>().Tree);
            c.AddTransient(p => p.GetRequiredService<Context>().Compilation);
            c.AddScoped<IValidationReporter, ValidationReporter>();
            c.AddScoped<SourceCodeStorage>();
            c.AddScoped<NodeLocator>();
            c.AddScoped<ValidationTypeDescriptorFactory>();
            c.AddScoped<ValidationControlTypeFactory>();
            c.AddScoped<ValidationControlMetadataFactory>();
            c.AddScoped<ValidationPropertyFactory>();
            c.AddScoped(p =>
            {
                var controlResolver = ActivatorUtilities.CreateInstance<ValidationControlResolver>(p);
                controlResolver.RegisterNamespace("dot", "DotVVM.Framework.Controls", "DotVVM.Framework");
                return controlResolver;
            });
            c.AddScoped<ValidationTreeResolver>();
            c.AddScoped<ValidationTreeBuilder>();
            c.AddScoped<DothtmlTokenizer>();
            c.AddScoped<DothtmlParser>();
            c.AddScoped<XPathTreeVisitor>();
            c.AddScoped<ErrorAggregatingVisitor>();
            c.AddScoped<XPathDothtmlNamespaceResolver>();
            c.AddScoped<NameTable>();
            return c.BuildServiceProvider();
        }

        private ImmutableArray<IValidationDiagnostic> GetValidationDiagnostics(IValidationReporter reporter)
        {
            return reporter.GetDiagnostics()
                .Where(d => d.Source == null || d.Source.IsValidated)
                .ToImmutableArray();
        }

        private ValidationTreeRoot GetValidationTree(
            IValidationReporter reporter,
            ValidationTreeResolver resolver,
            ErrorAggregatingVisitor visitor,
            DothtmlSourceCode sourceCode)
        {
            // TODO: Figure out how to handle master pages.

            // parse syntax
            var tokenizer = new DothtmlTokenizer();
            tokenizer.Tokenize(sourceCode.GetContent() ?? string.Empty);
            var parser = new DothtmlParser();
            var dothtmlRoot = parser.Parse(tokenizer.Tokens);

            // semantics
            var root = (ValidationTreeRoot)resolver.ResolveTree(dothtmlRoot, sourceCode.FileName);
            root.FileName = sourceCode.FileName;
            foreach (var diagnostic in visitor.Visit(root))
            {
                reporter.Report(diagnostic);
            }
            return root;
        }

        private XPathDothtmlRoot GetXPathTree(XPathTreeVisitor visitor, ValidationTreeRoot validationTree)
        {
            return visitor.Visit(validationTree);
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