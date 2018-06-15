using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlAttribute : XPathDothtmlNode
    {
        protected XPathDothtmlAttribute(ResolvedTreeNode node, XPathNodeType type) : base(node, type)
        {
        }

        public XPathDothtmlAttribute(ResolvedPropertySetter setter) : base(setter, XPathNodeType.Attribute)
        {
        }

        public XPathDothtmlAttribute FirstSibling { get; set; }

        public XPathDothtmlAttribute NextSibling { get; set; }

        public XPathDothtmlAttribute PreviousSibling { get; set; }
    }
}