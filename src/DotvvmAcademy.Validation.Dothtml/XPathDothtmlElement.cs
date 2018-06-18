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
            if (builder.Count == 0)
            {
                return;
            }
            Attributes = builder.ToImmutable();
            for (int i = 0; i < Attributes.Length; i++)
            {
                var attribute = Attributes[i];
                attribute.Root = Root;
                attribute.Parent = this;
                attribute.FirstSibling = Attributes[0];
                if (i > 0)
                {
                    attribute.PreviousSibling = Attributes[i - 1];
                }
                if (i < Attributes.Length - 1)
                {
                    attribute.NextSibling = Attributes[i + 1];
                }
            }
        }

        public void SetChildren(ImmutableArray<XPathDothtmlElement>.Builder builder)
        {
            if(builder.Count == 0)
            {
                return;
            }
            Children = builder.ToImmutable();
            for (int i = 0; i < Children.Length; i++)
            {
                var child = Children[i];
                child.Root = Root;
                child.Parent = this;
                child.FirstSibling = Children[0];
                if (i > 0)
                {
                    child.PreviousSibling = Children[i - 1];
                }
                if (i < Children.Length - 1)
                {
                    child.NextSibling = Children[i + 1];
                }
            }
        }
    }
}