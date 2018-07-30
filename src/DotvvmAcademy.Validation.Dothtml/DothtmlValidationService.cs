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
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationService : IValidationService<DothtmlUnit, DothtmlValidationOptions>
    {
        private readonly IServiceProvider globalProvider;

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
            return Task.Run(() =>
            {
                using (var scope = globalProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Context>();
                    context.Unit = unit;
                    context.Code = code;
                    context.Options = options;
                    HandleQueries<ValidationControl>(scope.ServiceProvider);
                    HandleQueries<ValidationPropertySetter>(scope.ServiceProvider);
                    HandleQueries<ValidationDirective>(scope.ServiceProvider);
                    return scope.ServiceProvider.GetRequiredService<ValidationReporter>().GetDiagnostics();
                }
            });
        }

        private IServiceProvider GetServiceProvider()
        {
            var c = new ServiceCollection();
            c.AddSingleton<ErrorAggregatingVisitor>();
            c.AddScoped(GetViewModelCompilation);
            c.AddScoped(GetValidationTree);
            c.AddScoped(GetXPathTree);
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
            c.AddScoped<Context>();
            c.AddScoped<NameTable>();
            return c.BuildServiceProvider();
        }

        private ValidationTreeRoot GetValidationTree(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var tokenizer = provider.GetRequiredService<DothtmlTokenizer>();
            tokenizer.Tokenize(context.Code ?? string.Empty);
            var parser = provider.GetRequiredService<DothtmlParser>();
            var dothtmlRoot = parser.Parse(tokenizer.Tokens);
            var resolver = provider.GetRequiredService<ValidationTreeResolver>();
            var root = (ValidationTreeRoot)resolver.ResolveTree(dothtmlRoot, context.Options.FileName);
            var reporter = provider.GetRequiredService<ValidationReporter>();
            if (context.Options.IncludeCompilerDiagnostics)
            {
                foreach (var diagnostic in resolver.GetDiagnostics())
                {
                    reporter.Report(diagnostic);
                }
                var visitor = provider.GetRequiredService<ErrorAggregatingVisitor>();
                foreach (var diagnostic in visitor.Visit(root))
                {
                    reporter.Report(diagnostic);
                }
            }

            return root;
        }

        private CSharpCompilation GetViewModelCompilation(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<Context>();
            var tree = CSharpSyntaxTree.ParseText(context.Options.ViewModel);
            return CSharpCompilation.Create(
                assemblyName: $"DotvvmAcademy.Validation.Dothtml.{context.Id}",
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
            var queries = unit.Queries.Values.OfType<DothtmlQuery<TResult>>();
            foreach (var query in queries)
            {
                var result = GetXPathResult(provider, query.XPath);
                var castResult = result.IsDefaultOrEmpty
                    ? ImmutableArray<TResult>.Empty
                    : result.Cast<TResult>().ToImmutableArray();
                var reporter = provider.GetRequiredService<ValidationReporter>();
                foreach (var constraint in query.Constraints)
                {
                    var context = new DothtmlConstraintContext<TResult>(provider, query.XPath, castResult);
                    constraint(context);
                }
            }
        }

        private class Context
        {
            public string Code { get; set; }

            public Guid Id { get; } = Guid.NewGuid();

            public DothtmlValidationOptions Options { get; set; }

            public DothtmlUnit Unit { get; set; }
        }
    }
}