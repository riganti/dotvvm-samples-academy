using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationService : IValidationService<DothtmlUnit>
    {
        private readonly IServiceProvider globalProvider;

        public DothtmlValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public Task<ImmutableArray<IValidationDiagnostic>> Validate(
            DothtmlUnit unit,
            ImmutableArray<ISourceCode> sources)
        {
            return Task.Run(() =>
            {
                using (var scope = globalProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Context>();
                    context.Unit = unit;
                    context.Sources = sources;
                    var compilationAccessor = scope.ServiceProvider.GetRequiredService<ICSharpCompilationAccessor>();
                    compilationAccessor.Compilation = GetCompilation(scope.ServiceProvider);
                    var assemblyAccessor = scope.ServiceProvider.GetRequiredService<IAssemblyAccessor>();
                    assemblyAccessor.Assemblies = Assembly.GetEntryAssembly()
                        .GetReferencedAssemblies()
                        .Select(Assembly.Load)
                        .ToImmutableArray();
                    HandleQueries<ValidationControl>(scope.ServiceProvider);
                    HandleQueries<ValidationPropertySetter>(scope.ServiceProvider);
                    HandleQueries<ValidationDirective>(scope.ServiceProvider);
                    return GetValidationDiagnostics(scope.ServiceProvider);
                }
            });
        }

        private CSharpCompilation GetCompilation(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var trees = context.Sources.OfType<CSharpSourceCode>()
                .Select(s => CSharpSyntaxTree.ParseText(s.GetContent()));
            return CSharpCompilation.Create(
                assemblyName: $"DotvvmAcademy.Validation.Dothtml.{context.Id}",
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
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddMetaSingletonFriendly();
            c.AddSingleton<ErrorAggregatingVisitor>();
            c.AddScoped(GetValidationTree);
            c.AddScoped(GetXPathTree);
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
            c.AddScoped<IValidationReporter>(p => p.GetRequiredService<DothtmlValidationReporter>());
            c.AddScoped<DothtmlValidationReporter>();
            c.AddScoped<DothtmlSourceCodeProvider>();
            c.AddScoped<XPathDothtmlNamespaceResolver>();
            c.AddScoped<Context>();
            c.AddScoped<NameTable>();
            return c.BuildServiceProvider();
        }

        private ImmutableArray<IValidationDiagnostic> GetValidationDiagnostics(IServiceProvider provider)
        {
            return provider.GetRequiredService<DothtmlValidationReporter>()
                .GetReportedDiagnostics()
                .Where(d => d.Source == null || d.Source.IsValidated)
                .ToImmutableArray();
        }

        private ValidationTreeRoot GetValidationTree(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var tokenizer = provider.GetRequiredService<DothtmlTokenizer>();
            // TODO: What about master pages?
            var sourceCode = context.Sources.OfType<DothtmlSourceCode>().Single();
            tokenizer.Tokenize(sourceCode.GetContent() ?? string.Empty);
            var parser = provider.GetRequiredService<DothtmlParser>();
            var dothtmlRoot = parser.Parse(tokenizer.Tokens);
            var resolver = provider.GetRequiredService<ValidationTreeResolver>();
            var root = (ValidationTreeRoot)resolver.ResolveTree(dothtmlRoot, ".dothtml");
            root.SourceCode = sourceCode;
            var reporter = provider.GetRequiredService<DothtmlValidationReporter>();
            var visitor = provider.GetRequiredService<ErrorAggregatingVisitor>();
            foreach (var diagnostic in visitor.Visit(root))
            {
                reporter.Report(diagnostic);
            }
            return root;
        }

        private ImmutableArray<ValidationTreeNode> GetXPathResult(IServiceProvider provider, string xpath)
        {
            var tree = provider.GetRequiredService<XPathDothtmlRoot>();
            var namespaceResolver = provider.GetRequiredService<XPathDothtmlNamespaceResolver>();
            var nameTable = provider.GetRequiredService<NameTable>();
            var navigator = new XPathDothtmlNavigator(nameTable, tree);
            var expression = XPathExpression.Compile(xpath, namespaceResolver);
            var result = (XPathNodeIterator)navigator.Evaluate(expression);
            var builder = ImmutableArray.CreateBuilder<ValidationTreeNode>();
            while (result.MoveNext())
            {
                var current = (XPathDothtmlNavigator)result.Current;
                builder.Add(current.Node.UnderlyingObject);
            }
            return builder.ToImmutable();
        }

        private XPathDothtmlRoot GetXPathTree(IServiceProvider provider)
        {
            var validationTree = provider.GetRequiredService<ValidationTreeRoot>();
            var visitor = provider.GetRequiredService<XPathTreeVisitor>();
            return visitor.Visit(validationTree);
        }

        private void HandleQueries<TResult>(IServiceProvider provider)
            where TResult : ValidationTreeNode
        {
            var unit = provider.GetRequiredService<Context>().Unit;
            foreach (var query in unit.GetQueries<TResult>())
            {
                var result = GetXPathResult(provider, query.Source).OfType<TResult>().ToImmutableArray();
                foreach (var constraint in query.GetConstraints())
                {
                    var context = new ConstraintContext<TResult>(provider, query, result);
                    constraint(context);
                }
            }
        }

        private class Context
        {
            public Guid Id { get; } = Guid.NewGuid();

            public ImmutableArray<ISourceCode> Sources { get; set; }

            public DothtmlUnit Unit { get; set; }
        }
    }
}