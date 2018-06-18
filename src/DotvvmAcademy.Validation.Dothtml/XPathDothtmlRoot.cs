using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlRoot : XPathDothtmlNode
    {
        public XPathDothtmlRoot(ResolvedTreeRoot root) : base(root, XPathNodeType.Root)
        {
            Root = this;
        }

        public NameTable NameTable { get; } = new NameTable();

        public override string ToString()
        {
            return "Root";
        }
    }
}