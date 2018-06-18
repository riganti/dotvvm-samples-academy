using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal abstract class XPathDothtmlAttribute : XPathDothtmlNode
    {
        protected XPathDothtmlAttribute(ResolvedTreeNode node, XPathNodeType type) : base(node, type)
        {
        }

        public XPathDothtmlAttribute FirstSibling { get; set; }

        public XPathDothtmlAttribute NextSibling { get; set; }

        public XPathDothtmlAttribute PreviousSibling { get; set; }

        public abstract object Value { get; }
    }
}