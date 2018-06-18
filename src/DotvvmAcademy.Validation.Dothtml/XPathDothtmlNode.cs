using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Text;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal abstract class XPathDothtmlNode
    {
        public XPathDothtmlNode(ResolvedTreeNode node, XPathNodeType type)
        {
            UnderlyingObject = node;
            NodeType = type;
        }

        public string LocalName { get; set; }

        public XPathNodeType NodeType { get; }

        public XPathDothtmlNode Parent { get; internal set; }

        public string Prefix { get; set; }

        public XPathDothtmlRoot Root { get; internal set; }

        public ResolvedTreeNode UnderlyingObject { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
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