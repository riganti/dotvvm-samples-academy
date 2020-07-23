using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Immutable;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class XPathTreeVisitor
    {
        private readonly XPathDothtmlNamespaceResolver namespaceResolver;
        private readonly NameTable nameTable;
        private XPathDothtmlRoot root;

        public XPathTreeVisitor(NameTable nameTable, XPathDothtmlNamespaceResolver namespaceResolver)
        {
            this.nameTable = nameTable;
            this.namespaceResolver = namespaceResolver;
        }

        public XPathDothtmlRoot Visit(ValidationTreeRoot tree)
        {
            root = new XPathDothtmlRoot(tree);
            if (!tree.Content.IsDefaultOrEmpty)
            {
                var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var child in tree.Content)
                {
                    var controlNode = VisitControl(child);
                    if (controlNode != null)
                    {
                        children.Add(controlNode);
                    }
                }
                root.SetChildren(children);
            }

            if (!tree.Directives.IsDefaultOrEmpty)
            {
                var attributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var directive in tree.Directives)
                {
                    attributes.Add(VisitDirective(directive));
                }
                root.SetAttributes(attributes);
            }

            return root;
        }

        private string GetPropertyName(IPropertyDescriptor property)
        {
            string name = property switch
            {
                ValidationAttachedProperty attachedProperty => $"{attachedProperty.DeclaringType.Name}.{property.Name}",
                _ => property.Name,
            };
            return nameTable.GetOrAdd(name);
        }

        private XPathDothtmlNode VisitControl(ValidationControl control)
        {
            XPathDothtmlNode node;
            if (control.Metadata.Type.IsEqualTo(typeof(RawLiteral)))
            {
                return VisitRawLiteral(control);
            }
            else if (control.Metadata.Type.IsEqualTo(typeof(Literal)))
            {
                node = VisitLiteral(control);
            }
            else if (control.Metadata.Type.IsEqualTo(typeof(HtmlGenericControl)))
            {
                node = VisitHtmlGenericControl(control);
            }
            else
            {
                node = new XPathDothtmlNode(control, XPathNodeType.Element)
                {
                    LocalName = nameTable.GetOrAdd(control.Metadata.Type.Name),
                    Namespace = nameTable.GetOrAdd(control.Metadata.Type.Namespace),
                    Prefix = nameTable.GetOrAdd(namespaceResolver.LookupPrefix(control.Metadata.Type.Namespace))
                };
            }

            if (!control.Content.IsDefaultOrEmpty)
            {
                var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var child in control.Content)
                {
                    var controlNode = VisitControl(child);
                    if (controlNode != null)
                    {
                        children.Add(controlNode);
                    }
                }
                node.SetChildren(children);
            }

            if (!control.PropertySetters.IsDefaultOrEmpty)
            {
                var attributes = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var property in control.PropertySetters)
                {
                    attributes.Add(VisitProperty(property));
                }
                node.SetAttributes(attributes);
            }

            return node;
        }

        private XPathDothtmlNode VisitDirective(ValidationDirective directive)
        {
            var syntax = (DothtmlDirectiveNode)directive.DothtmlNode;
            return new XPathDothtmlNode(directive, XPathNodeType.Attribute)
            {
                LocalName = nameTable.GetOrAdd(syntax.Name),
                Value = directive.Value
            };
        }

        private XPathDothtmlNode VisitHtmlGenericControl(ValidationControl control)
        {
            var elementNode = (DothtmlElementNode)control.DothtmlNode;
            return new XPathDothtmlNode(control, XPathNodeType.Element)
            {
                LocalName = nameTable.GetOrAdd(elementNode.TagName)
            };
        }

        private XPathDothtmlNode VisitLiteral(ValidationControl control)
        {
            var localName = nameTable.GetOrAdd(control.Metadata.Type.Name);
            var @namespace = nameTable.GetOrAdd(control.Metadata.Type.Namespace);
            var prefix = nameTable.GetOrAdd(namespaceResolver.LookupPrefix(@namespace));
            return new XPathDothtmlNode(control, XPathNodeType.Element)
            {
                LocalName = localName,
                Namespace = @namespace,
                Prefix = prefix
            };
        }

        private XPathDothtmlNode VisitProperty(ValidationPropertySetter property)
        {
            return property switch
            {
                ValidationPropertyBinding binding => VisitPropertyBinding(binding),
                ValidationPropertyControl control => VisitPropertyControl(control),
                ValidationPropertyControlCollection controlCollection => VisitPropertyControlCollection(controlCollection),
                ValidationPropertyTemplate template => VisitPropertyTemplate(template),
                ValidationPropertyValue value => VisitPropertyValue(value),
                _ => throw new ArgumentException($"Property setter of type '{property.GetType().Name}' " +
                    $"is not supported"),
            };
        }

        private XPathDothtmlNode VisitPropertyBinding(ValidationPropertyBinding propertyBinding)
        {
            return new XPathDothtmlNode(propertyBinding, XPathNodeType.Attribute)
            {
                LocalName = GetPropertyName(propertyBinding.Property),
                Value = propertyBinding.Binding
            };
        }

        private XPathDothtmlNode VisitPropertyControl(ValidationPropertyControl propertyControl)
        {
            var node = new XPathDothtmlNode(propertyControl, XPathNodeType.Attribute)
            {
                LocalName = GetPropertyName(propertyControl.Property),
            };
            var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
            var controlNode = VisitControl(propertyControl.Control);
            if (controlNode != null)
            {
                children.Add(controlNode);
            }
            node.SetChildren(children);
            node.Value = node.Children;
            return node;
        }

        private XPathDothtmlNode VisitPropertyControlCollection(ValidationPropertyControlCollection propertyCollection)
        {
            var node = new XPathDothtmlNode(propertyCollection, XPathNodeType.Attribute)
            {
                LocalName = GetPropertyName(propertyCollection.Property),
            };
            if (!propertyCollection.Controls.IsDefaultOrEmpty)
            {
                var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var child in propertyCollection.Controls)
                {
                    var controlNode = VisitControl(child);
                    if (controlNode != null)
                    {
                        children.Add(controlNode);
                    }
                }
                node.SetChildren(children);
                node.Value = node.Children;
            }

            return node;
        }

        private XPathDothtmlNode VisitPropertyTemplate(ValidationPropertyTemplate propertyTemplate)
        {
            var node = new XPathDothtmlNode(propertyTemplate, XPathNodeType.Attribute)
            {
                LocalName = GetPropertyName(propertyTemplate.Property),
            };
            if (!propertyTemplate.Content.IsDefaultOrEmpty)
            {
                var children = ImmutableArray.CreateBuilder<XPathDothtmlNode>();
                foreach (var child in propertyTemplate.Content)
                {
                    var controlNode = VisitControl(child);
                    if (controlNode != null)
                    {
                        children.Add(controlNode);
                    }
                }
                node.SetChildren(children);
                node.Value = node.Children;
            }

            return node;
        }

        private XPathDothtmlNode VisitPropertyValue(ValidationPropertyValue propertyValue)
        {
            return new XPathDothtmlNode(propertyValue, XPathNodeType.Attribute)
            {
                LocalName = GetPropertyName(propertyValue.Property),
                Value = propertyValue.Value
            };
        }

        private XPathDothtmlNode VisitRawLiteral(ValidationControl control)
        {
            if (control.ConstructorParameters == null
                || control.ConstructorParameters.Length != 3
                || string.IsNullOrWhiteSpace(control.ConstructorParameters[0] as string)
                || control.ConstructorParameters[2] as bool? == true)
            {
                // this RawLiteral is not validation-worthy
                return null;
            }

            return new XPathDothtmlNode(control, XPathNodeType.Element)
            {
                LocalName = nameof(RawLiteral),
                Value = (string)control.ConstructorParameters[0]
            };
        }
    }
}
