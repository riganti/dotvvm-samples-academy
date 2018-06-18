using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathResolvedTreeVisitor : IResolvedControlTreeVisitor
    {
        private ImmutableArray<XPathDothtmlAttribute>.Builder currentAttributes;
        private ImmutableArray<XPathDothtmlElement>.Builder currentChildren;

        public XPathDothtmlRoot Root { get; private set; }

        public void VisitBinding(ResolvedBinding binding)
        {
        }

        public void VisitControl(ResolvedControl control)
        {
            var parentChildren = currentChildren;
            var parentAttributes = currentAttributes;
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlElement>();
            currentAttributes = ImmutableArray.CreateBuilder<XPathDothtmlAttribute>();
            var current = new XPathDothtmlElement(control);
            if (control.DothtmlNode is DothtmlElementNode elementNode)
            {
                current.LocalName = GetOrAdd(elementNode.TagName);
                current.Prefix = GetOrAdd(elementNode.TagPrefix);
            }
            else
            {
                current.LocalName = GetOrAdd(control.Metadata.Name);
                current.Prefix = GetOrAdd(control.Metadata.Namespace);
            }
            parentChildren.Add(current);
            control.AcceptChildren(this);
            current.SetChildren(currentChildren);
            current.SetAttributes(currentAttributes);
            currentChildren = parentChildren;
            currentAttributes = parentAttributes;
        }

        public void VisitDirective(ResolvedDirective directive)
        {
        }

        public void VisitImportDirective(ResolvedImportDirective importDirective)
        {
        }

        public void VisitPropertyBinding(ResolvedPropertyBinding propertyBinding)
        {
        }

        public void VisitPropertyControl(ResolvedPropertyControl propertyControl)
        {
        }

        public void VisitPropertyControlCollection(ResolvedPropertyControlCollection propertyControlCollection)
        {
        }

        public void VisitPropertyTemplate(ResolvedPropertyTemplate propertyTemplate)
        {
        }

        public void VisitPropertyValue(ResolvedPropertyValue propertyValue)
        {
        }

        public void VisitView(ResolvedTreeRoot view)
        {
            Root = new XPathDothtmlRoot(view);
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlElement>();
            currentAttributes = ImmutableArray.CreateBuilder<XPathDothtmlAttribute>();
            view.AcceptChildren(this);
            Root.SetChildren(currentChildren);
            Root.SetAttributes(currentAttributes);
        }

        private string GetOrAdd(string value)
        {
            if (value == null)
            {
                return null;
            }
            var existingValue = Root.NameTable.Get(value);
            if (existingValue != null)
            {
                return existingValue;
            }
            return Root.NameTable.Add(value);
        }
    }
}