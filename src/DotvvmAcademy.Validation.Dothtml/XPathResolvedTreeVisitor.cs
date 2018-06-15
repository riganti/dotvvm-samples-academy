using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathResolvedTreeVisitor : ResolvedControlTreeVisitor
    {
        private ImmutableArray<XPathDothtmlElement>.Builder currentChildren;
        private ImmutableArray<XPathDothtmlAttribute>.Builder currentAttributes;

        public XPathDothtmlElement Root { get; private set; }

        public override void VisitView(ResolvedTreeRoot view)
        {
            Root = new XPathDothtmlRoot
            {
                UnderlyingObject = view,
                
            };
            var children = ImmutableArray.CreateBuilder<XPathDothtmlElement>();
            var attributes = ImmutableArray.CreateBuilder<XPathDothtmlAttribute>();
            currentChildren = children;
            currentAttributes = attributes;
            base.VisitView(view);
            Root.SetChildren(children);
            Root.SetAttributes(attributes);
        }

        public override void VisitControl(ResolvedControl control)
        {
            var current = new XPathDothtmlElement
            {

            }
            base.VisitControl(control);
        }
    }
}