using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Immutable;
using System.Text;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class XPathDothtmlNode
    {
        public XPathDothtmlNode(ValidationTreeNode node, XPathNodeType type)
        {
            UnderlyingObject = node;
            NodeType = type;
        }

        public ImmutableArray<XPathDothtmlNode> Attributes { get; private set; }

        public XPathDothtmlNode FirstSibling { get; private set; }

        public string Namespace { get; set; }

        public ImmutableArray<XPathDothtmlNode> Children { get; private set; }

        public string LocalName { get; set; }

        public XPathDothtmlNode NextSibling { get; private set; }

        public XPathNodeType NodeType { get; }

        public XPathDothtmlNode Parent { get; internal set; }

        public string Prefix { get; set; }

        public XPathDothtmlNode PreviousSibling { get; private set; }

        public XPathDothtmlRoot Root { get; internal set; }

        public ValidationTreeNode UnderlyingObject { get; }

        public object Value { get; set; }

        public void SetAttributes(ImmutableArray<XPathDothtmlNode>.Builder builder) => Attributes = SetupNodeArray(builder);

        public void SetChildren(ImmutableArray<XPathDothtmlNode>.Builder builder) => Children = SetupNodeArray(builder);

        private ImmutableArray<XPathDothtmlNode> SetupNodeArray(ImmutableArray<XPathDothtmlNode>.Builder builder)
        {
            if (builder.Count == 0)
            {
                return default;
            }
            var array = builder.ToImmutable();
            for (int i = 0; i < array.Length; i++)
            {
                var node = array[i];
                node.Root = Root;
                node.Parent = this;
                node.FirstSibling = array[0];
                if (i > 0)
                {
                    node.PreviousSibling = array[i - 1];
                }
                if (i < array.Length - 1)
                {
                    node.NextSibling = array[i + 1];
                }
            }
            return array;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(NodeType.ToString());
            sb.Append(": ");
            if (Prefix != null)
            {
                sb.Append(Prefix);
                sb.Append(':');
            }
            sb.Append(LocalName);
            return sb.ToString();
        }
    }
}