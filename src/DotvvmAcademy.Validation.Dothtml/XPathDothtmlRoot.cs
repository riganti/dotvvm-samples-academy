using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlRoot : XPathDothtmlElement
    {
        public XPathDothtmlRoot(ResolvedTreeRoot root) : base(root, XPathNodeType.Root)
        {
            Root = this;
        }

        protected XPathDothtmlRoot(ResolvedTreeNode node, XPathNodeType type) : base(node, type)
        {
        }

        public NameTable NameTable { get; } = new NameTable();

        public override string ToString()
        {
            return "Root";
        }
    }
}