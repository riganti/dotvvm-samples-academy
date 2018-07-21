using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationService : IValidationService<DothtmlUnit, DothtmlValidationOptions>
    {
        private readonly IServiceProvider globalProvider;
        private string code;
        private Guid id;
        private DothtmlValidationOptions options;
        private IServiceProvider provider;
        private DothtmlUnit unit;

        public DothtmlValidationService()
        {
            globalProvider = GetServiceProvider();
        }

        public Task<ImmutableArray<IValidationDiagnostic>> Validate(
            DothtmlUnit unit,
            string code,
            DothtmlValidationOptions options = null)
        {
            options = options ?? DothtmlValidationOptions.Default;
            this.unit = unit;
            this.code = code;
            this.options = options;
            id = Guid.NewGuid();
            using (var scope = globalProvider.CreateScope())
            {
                provider = scope.ServiceProvider;
                var validationTree = GetValidationTree();
                var xpathTree = GetXPathTree(validationTree);
                var controlQueries = unit.Queries.Values.OfType<DothtmlQuery<ValidationControl>>().ToImmutableArray();
                HandleQueries(xpathTree, controlQueries);
                var propertyQueries = unit.Queries.Values
                    .OfType<DothtmlQuery<ValidationPropertySetter>>()
                    .ToImmutableArray();
                HandleQueries(xpathTree, propertyQueries);
                var directiveQueries = unit.Queries.Values
                    .OfType<DothtmlQuery<ValidationDirective>>()
                    .ToImmutableArray();
                HandleQueries(xpathTree, directiveQueries);
                var reporter = provider.GetRequiredService<ValidationReporter>();
                return Task.FromResult(reporter.GetDiagnostics());
            }
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddScoped(p => GetViewModelCompilation());
            c.AddScoped(p => options);
            c.AddScoped<AttributeExtractor>();
            c.AddScoped<ValidationTypeDescriptorFactory>();
            c.AddScoped<ValidationControlTypeFactory>();
            c.AddScoped<ValidationControlMetadataFactory>();
            c.AddScoped<ValidationPropertyDescriptorFactory>();
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
            c.AddScoped<ValidationReporter>();
            c.AddScoped<XPathDothtmlNamespaceResolver>();
            return c.BuildServiceProvider();
        }

        private ValidationTreeRoot GetValidationTree()
        {
            var tokenizer = provider.GetRequiredService<DothtmlTokenizer>();
            tokenizer.Tokenize(code);
            var parser = provider.GetRequiredService<DothtmlParser>();
            var dothtmlRoot = parser.Parse(tokenizer.Tokens);
            var resolver = provider.GetRequiredService<ValidationTreeResolver>();
            return (ValidationTreeRoot)resolver.ResolveTree(dothtmlRoot, options.FileName);
        }

        private CSharpCompilation GetViewModelCompilation()
        {
            var tree = CSharpSyntaxTree.ParseText(options.ViewModel);
            return CSharpCompilation.Create(
                assemblyName: $"DotvvmAcademy.Validation.Dothtml.{id}",
                syntaxTrees: new[] { tree },
                references: new[]
                {
                    MetadataReferencer.FromName("mscorlib"),
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("System.Collections"),
                    MetadataReferencer.FromName("System.Reflection"),
                    MetadataReferencer.FromName("DotVVM.Framework"),
                    MetadataReferencer.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        }

        private ImmutableArray<ValidationTreeNode> GetXPathResult(string xpath, XPathDothtmlRoot tree)
        {
            var namespaceResolver = provider.GetRequiredService<XPathDothtmlNamespaceResolver>();
            var navigator = new XPathDothtmlNavigator(tree);
            var expression = XPathExpression.Compile(xpath, namespaceResolver);
            var result = navigator.Evaluate(expression);

            throw new NotImplementedException();
        }

        private XPathDothtmlRoot GetXPathTree(ValidationTreeRoot validationTree)
        {
            var visitor = provider.GetRequiredService<XPathTreeVisitor>();
            return visitor.Visit(validationTree);
        }

        private void HandleQueries<TResult>(XPathDothtmlRoot xpathTree, ImmutableArray<DothtmlQuery<TResult>> queries)
            where TResult : ValidationTreeNode
        {
            foreach (var query in queries)
            {
                var result = GetXPathResult(query.XPath, xpathTree).CastArray<TResult>();
                var reporter = provider.GetRequiredService<ValidationReporter>();
                foreach (var constraint in query.Constraints)
                {
                    var context = new DothtmlConstraintContext<TResult>(reporter, query.XPath, result);
                    constraint(context);
                }
            }
        }
    }
}