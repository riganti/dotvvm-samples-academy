using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Immutable;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathTreeVisitor
    {
        private XPathDothtmlRoot root;

        public XPathDothtmlRoot Visit(ValidationTreeRoot tree)
        {
            root = new XPathDothtmlRoot(tree);
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var attributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            foreach (var child in tree.Content)
            {
                children.Add(VisitControl(child));
            }
            foreach (var directive in tree.Directives)
            {
                attributes.Add(VisitDirective(directive));
            }
            root.SetChildren(children);
            root.SetAttributes(attributes);
            return root;
        }

        private XPathDothtmlNode VisitControl(ValidationControl control)
        {
            var node = new XPathDothtmlNode(control, XPathNodeType.Element);
            if (control.DothtmlNode is DothtmlElementNode elementNode)
            {
                node.LocalName = AddString(elementNode.TagName);
                node.Prefix = AddString(elementNode.TagPrefix);
            }
            else
            {
                node.LocalName = AddString(control.Metadata.Type.Name);
                node.Prefix = AddString(control.Metadata.Type.Namespace);
            }
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var attributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            foreach (var child in control.Content)
            {
                children.Add(VisitControl(child));
            }
            foreach (var property in control.PropertySetters)
            {
                children.Add(VisitProperty(property));
            }
            node.SetChildren(children);
            node.SetAttributes(attributes);
            return node;
        }

        private XPathDothtmlNode VisitDirective(ValidationDirective directive)
        {
            var syntax = (DothtmlDirectiveNode)directive.DothtmlNode;
            return new XPathDothtmlNode(directive, XPathNodeType.Attribute)
            {
                LocalName = AddString(syntax.Name),
                Value = directive.Value
            };
        }

        private XPathDothtmlNode VisitProperty(ValidationPropertySetter property)
        {
            switch (property)
            {
                case ValidationPropertyBinding binding:
                    return VisitPropertyBinding(binding);

                case ValidationPropertyControl control:
                    return VisitPropertyControl(control);

                case ValidationPropertyControlCollection controlCollection:
                    return VisitPropertyControlCollection(controlCollection);

                case ValidationPropertyTemplate template:
                    return VisitPropertyTemplate(template);

                case ValidationPropertyValue value:
                    return VisitPropertyValue(value);

                default:
                    throw new ArgumentException($"Property setter of type '{property.GetType().Name}' is not supported");
            }
        }

        private XPathDothtmlNode VisitPropertyBinding(ValidationPropertyBinding propertyBinding)
        {
            return new XPathDothtmlNode(propertyBinding, XPathNodeType.Attribute)
            {
                LocalName = AddString(propertyBinding.Property.Name),
                Value = propertyBinding.Binding
            };
        }

        private XPathDothtmlNode VisitPropertyControl(ValidationPropertyControl propertyControl)
        {
            var node = new XPathDothtmlNode(propertyControl, XPathNodeType.Attribute)
            {
                LocalName = AddString(propertyControl.Property.Name),
            };
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            children.Add(VisitControl(propertyControl.Control));
            node.SetChildren(children);
            node.Value = node.Children;
            return node;
        }

        private XPathDothtmlNode VisitPropertyControlCollection(ValidationPropertyControlCollection propertyControlCollection)
        {
            var node = new XPathDothtmlNode(propertyControlCollection, XPathNodeType.Attribute)
            {
                LocalName = AddString(propertyControlCollection.Property.Name),
            };
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            foreach(var child in propertyControlCollection.Controls)
            {
                children.Add(VisitControl(child));
            }
            node.SetChildren(children);
            node.Value = node.Children;
            return node;
        }

        private XPathDothtmlNode VisitPropertyTemplate(ValidationPropertyTemplate propertyTemplate)
        {
            var node = new XPathDothtmlNode(propertyTemplate, XPathNodeType.Attribute)
            {
                LocalName = AddString(propertyTemplate.Property.Name),
            };
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            foreach(var child in propertyTemplate.Content)
            {
                children.Add(VisitControl(child));
            }
            node.SetChildren(children);
            node.Value = node.Children;
            return node;
        }

        private XPathDothtmlNode VisitPropertyValue(ValidationPropertyValue propertyValue)
        {
            var syntax = (DothtmlAttributeNode)propertyValue.DothtmlNode;
            return new XPathDothtmlNode(propertyValue, XPathNodeType.Attribute)
            {
                LocalName = AddString(propertyValue.Property.Name),
                Value = propertyValue.Value
            };
        }

        private string AddString(string value)
        {
            if (value == null)
            {
                return null;
            }
            var existingValue = root.NameTable.Get(value);
            if (existingValue != null)
            {
                return existingValue;
            }
            return root.NameTable.Add(value);
        }
    }
}