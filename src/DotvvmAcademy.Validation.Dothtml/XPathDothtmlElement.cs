using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Immutable;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlElement : XPathDothtmlNode
    {
        public XPathDothtmlElement(ResolvedControl control) : base(control, XPathNodeType.Element)
        {
        }

        protected XPathDothtmlElement(ResolvedTreeNode node, XPathNodeType type) : base(node, type)
        {
        }

        public ImmutableArray<XPathDothtmlAttribute> Attributes { get; private set; }

        public XPathDothtmlElement FirstSibling { get; private set; }

        public ImmutableArray<XPathDothtmlElement> Children { get; private set; }

        public XPathDothtmlElement NextSibling { get; private set; }

        public XPathDothtmlElement PreviousSibling { get; private set; }

        public void SetAttributes(ImmutableArray<XPathDothtmlAttribute>.Builder builder)
        {
        }

        public void SetChildren(ImmutableArray<XPathDothtmlElement>.Builder builder)
        {
        }
    }
}