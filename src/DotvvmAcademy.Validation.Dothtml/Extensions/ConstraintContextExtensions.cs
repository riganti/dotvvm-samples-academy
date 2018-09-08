using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static ImmutableArray<ValidationTreeNode> Locate(this ConstraintContext context, XPathExpression xpath)
        {
            var tree = context.Provider.GetRequiredService<XPathDothtmlRoot>();
            var namespaceResolver = context.Provider.GetRequiredService<XPathDothtmlNamespaceResolver>();
            var nameTable = context.Provider.GetRequiredService<NameTable>();
            var navigator = new XPathDothtmlNavigator(nameTable, tree);
            xpath.SetContext(namespaceResolver);
            var result = (XPathNodeIterator)navigator.Evaluate(xpath);
            var builder = ImmutableArray.CreateBuilder<ValidationTreeNode>();
            while (result.MoveNext())
            {
                var current = (XPathDothtmlNavigator)result.Current;
                builder.Add(current.Node.UnderlyingObject);
            }
            return builder.ToImmutable();
        }

        public static ImmutableArray<TResult> Locate<TResult>(this ConstraintContext context, XPathExpression xpath)
            where TResult : ValidationTreeNode
        {
            return context.Locate(xpath).OfType<TResult>().ToImmutableArray();
        }

        public static void Report(
            this ConstraintContext context,
            string message,
            IEnumerable<object> arguments,
            ValidationTreeNode node,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<DothtmlValidationReporter>().Report(message, arguments, node, severity);
        }

        public static void Report(
            this ConstraintContext context,
            string message,
            ValidationTreeNode node,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<DothtmlValidationReporter>().Report(message, node, severity);
        }
    }
}