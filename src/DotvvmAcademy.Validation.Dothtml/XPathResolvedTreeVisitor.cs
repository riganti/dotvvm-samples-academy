using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Immutable;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathResolvedTreeVisitor : IResolvedControlTreeVisitor
    {
        private ImmutableArray<XPathDothtmlNode>.Builder currentAttributes;
        private ImmutableArray<XPathDothtmlNode>.Builder currentChildren;

        public XPathDothtmlRoot Root { get; private set; }

        public void VisitBinding(ResolvedBinding binding)
        {
        }

        public void VisitControl(ResolvedControl control)
        {
            var parentChildren = currentChildren;
            var parentAttributes = currentAttributes;
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            currentAttributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var current = new XPathDothtmlNode(control, XPathNodeType.Element);
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
            var syntax = (DothtmlDirectiveNode)directive.DothtmlNode;
            var attribute = new XPathDothtmlNode(directive, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(syntax.Name),
                Value = directive.Value
            };
            currentAttributes.Add(attribute);
        }

        public void VisitImportDirective(ResolvedImportDirective importDirective)
        {
        }

        public void VisitPropertyBinding(ResolvedPropertyBinding propertyBinding)
        {
            var attribute = new XPathDothtmlNode(propertyBinding, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(propertyBinding.Property.Name),
                Value = propertyBinding.Binding
            };
            currentAttributes.Add(attribute);
        }

        public void VisitPropertyControl(ResolvedPropertyControl propertyControl)
        {
            var parentChildren = currentChildren;
            var parentAttributes = currentAttributes;
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var attribute = new XPathDothtmlNode(propertyControl, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(propertyControl.Property.Name),
            };
            parentAttributes.Add(attribute);
            propertyControl.AcceptChildren(this);
            attribute.SetChildren(currentChildren);
            attribute.Value = attribute.Children;
            currentChildren = parentChildren;
            currentAttributes = parentAttributes;
        }

        public void VisitPropertyControlCollection(ResolvedPropertyControlCollection propertyControlCollection)
        {
            var parentChildren = currentChildren;
            var parentAttributes = currentAttributes;
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var attribute = new XPathDothtmlNode(propertyControlCollection, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(propertyControlCollection.Property.Name),
            };
            parentAttributes.Add(attribute);
            propertyControlCollection.AcceptChildren(this);
            attribute.SetChildren(currentChildren);
            attribute.Value = attribute.Children;
            currentChildren = parentChildren;
            currentAttributes = parentAttributes;
        }

        public void VisitPropertyTemplate(ResolvedPropertyTemplate propertyTemplate)
        {
            var parentChildren = currentChildren;
            var parentAttributes = currentAttributes;
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var attribute = new XPathDothtmlNode(propertyTemplate, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(propertyTemplate.Property.Name),
            };
            parentAttributes.Add(attribute);
            propertyTemplate.AcceptChildren(this);
            attribute.SetChildren(currentChildren);
            attribute.Value = attribute.Children;
            currentChildren = parentChildren;
            currentAttributes = parentAttributes;
        }

        public void VisitPropertyValue(ResolvedPropertyValue propertyValue)
        {
            var syntax = (DothtmlAttributeNode)propertyValue.DothtmlNode;
            var attribute = new XPathDothtmlNode(propertyValue, XPathNodeType.Attribute)
            {
                LocalName = GetOrAdd(propertyValue.Property.Name),
                Value = propertyValue.Value
            };
            currentAttributes.Add(attribute);
        }

        public void VisitView(ResolvedTreeRoot view)
        {
            Root = new XPathDothtmlRoot(view);
            currentChildren = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            currentAttributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
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