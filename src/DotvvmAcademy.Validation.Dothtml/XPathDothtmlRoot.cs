using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlRoot : XPathDothtmlElement
    {
        public XPathDothtmlRoot(ResolvedTreeRoot root) : base(root, XPathNodeType.Root)
        {
        }

        protected XPathDothtmlRoot(ResolvedTreeNode node, XPathNodeType type) : base(node, type)
        {
        }

        public NameTable NameTable { get; } = new NameTable();
    }
}