using DotVVM.Framework.Compilation.ControlTree.Resolved;
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

        protected XPathDothtmlNode()
        {
        }

        public string LocalName { get; set; }

        public XPathNodeType NodeType { get; protected set; }

        public XPathDothtmlElement Parent { get; protected set; }

        public string Prefix { get; set; }

        public XPathDothtmlRoot Root { get; protected set; }

        public ResolvedTreeNode UnderlyingObject { get; set; }
    }
}