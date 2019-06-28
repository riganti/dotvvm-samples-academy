using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class NodeLocator
    {
        private readonly XPathDothtmlNamespaceResolver namespaceResolver;
        private readonly NameTable nameTable;
        private readonly XPathDothtmlRoot tree;

        public NodeLocator(XPathDothtmlNamespaceResolver namespaceResolver, NameTable nameTable, XPathDothtmlRoot tree)
        {
            this.namespaceResolver = namespaceResolver;
            this.nameTable = nameTable;
            this.tree = tree;
        }

        public IEnumerable<ValidationTreeNode> Locate(XPathExpression expression)
        {
            expression.SetContext(namespaceResolver);
            var navigator = new XPathDothtmlNavigator(nameTable, tree);
            var result = (XPathNodeIterator)navigator.Evaluate(expression);

            var builder = ImmutableArray.CreateBuilder<ValidationTreeNode>();
            while (result.MoveNext())
            {
                var current = (XPathDothtmlNavigator)result.Current;
                builder.Add(current.Node.UnderlyingObject);
            }
            return builder.ToImmutable();
        }
    }
}