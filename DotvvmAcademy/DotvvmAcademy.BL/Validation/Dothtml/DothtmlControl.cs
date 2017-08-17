using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControl
    {
        private DothtmlValidate validate;

        public DothtmlControl(ResolvedContentNode node, DothtmlValidate validate)
        {
            Node = node;
            this.validate = validate;
        }

        public static DothtmlControl Inactive => new DothtmlControl(null, null) { IsActive = false };

        public bool IsActive { get; private set; } = true;

        public ResolvedContentNode Node { get; }

        public void Attribute(string name, string expectedValue)
        {
            if (!IsActive) return;

            var node = (DothtmlElementNode)Node.DothtmlNode;
            var attribute = node.Attributes.SingleOrDefault(a => a.AttributeName == name);

            if (attribute == null)
            {
                AddError($"This control should have the {name} attribute set.");
                return;
            }

            if (TokensToString(attribute.ValueNode.Tokens) != expectedValue)
            {
                AddError($"The {name} attribute contains incorrect value.", attribute.AttributeNameNode.StartPosition, attribute.AttributeNameNode.EndPosition);
            }
        }

        public DothtmlControl Control<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return Inactive;

            var controls = GetControls<TControl>().ToList();
            if (controls.Count > 1)
            {
                AddError($"This control should contain only one control of type '{typeof(TControl).Name}'.");
                return Inactive;
            }

            if (controls.Count == 0)
            {
                AddError($"This control should contain a control of type '{typeof(TControl).Name}'.");
                return Inactive;
            }

            return controls.Single();
        }

        public DothtmlControlCollection Controls<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls<TControl>();
            if (controls.Any())
            {
                return controls;
            }
            else
            {
                AddError($"This control doesn't contain any controls of type {typeof(TControl).Name}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Controls<TControl>(int count) where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls<TControl>();
            if (controls.Count() == count)
            {
                return controls;
            }
            else
            {
                AddError($"This control should contain {count} controls of type {typeof(TControl).Name}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControl Element(string elementTag)
        {
            if (!IsActive) return Inactive;

            var elements = GetElements(elementTag).ToList();
            if (elements.Count > 1)
            {
                AddError($"This control should contain only one element with tag '{elementTag}'.");
                return Inactive;
            }

            if (elements.Count == 0)
            {
                AddError($"This control should contain an element with tag '{elementTag}'.");
                return Inactive;
            }

            return elements.Single();
        }

        public DothtmlControlCollection Elements(string elementTag)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = GetElements(elementTag);
            if (elements.Any())
            {
                return elements;
            }
            else
            {
                AddError($"This control doesn't contain any elements with tag {elementTag}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Elements(string elementTag, int count)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = GetElements(elementTag);
            if (elements.Count() == count)
            {
                return elements;
            }
            else
            {
                AddError($"This control should contain {count} elements with tag {elementTag}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public void ValueBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a value binding on this control's '{property.Name}' property.";
            Binding<ValueBindingExpression>(property, message, expectedValues);
        }

        public void CommandBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a command binding on this control's '{property.Name}' property.";
            Binding<CommandBindingExpression>(property, message, expectedValues);
        }

        public void StaticCommandBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a static command binding on this control's '{property.Name}' property.";
            Binding<StaticCommandBindingExpression>(property, message, expectedValues);
        }

        public void ResourceBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a resource binding on this control's '{property.Name}' property.";
            Binding<ResourceBindingExpression>(property, message, expectedValues);
        }

        public void HardcodedValue(IPropertyDescriptor property, params object[] expectedValues)
        {
            if (!IsActive) return;

            var control = (ResolvedControl)Node;
            if (!control.TryGetProperty(property, out var setter) || !(setter is ResolvedPropertyValue))
            {
                AddError($"There should be a hard-coded value set on this control's '{property.Name}' property.");
                return;
            }

            var value = (ResolvedPropertyValue)setter;
            if (expectedValues.Contains(value.Value))
            {
                AddError($"This property contains an incorrect value.",
                    value.DothtmlNode.StartPosition, value.DothtmlNode.EndPosition);
            }
        }

        private void Binding<TBinding>(IPropertyDescriptor property, string wrongTypeErrorMessage, params string[] expectedValues)
        where TBinding : BindingExpression
        {
            if (!IsActive) return;

            var control = (ResolvedControl)Node;
            if (!control.TryGetProperty(property, out var setter) || !(setter is ResolvedPropertyBinding))
            {
                AddError($"There should be a binding set on this control's '{property.Name}' property.");
                return;
            }

            var binding = (ResolvedPropertyBinding)setter;

            if (binding.Binding.BindingType != typeof(ValueBindingExpression))
            {
                AddError(wrongTypeErrorMessage, binding.DothtmlNode.StartPosition, binding.DothtmlNode.EndPosition);
            }

            if (!expectedValues.Contains(binding.Binding.Value.Trim()))
            {
                AddError($"This binding contains an incorrect value.",
                    binding.Binding.BindingNode.StartPosition, binding.Binding.BindingNode.EndPosition);
            }
        }

        private void AddError(string message)
        {
            validate.AddError(message, Node.DothtmlNode.StartPosition, Node.DothtmlNode.EndPosition);
        }

        private void AddError(string message, int startPosition, int endPosition)
        {
            validate.AddError(message, startPosition, endPosition);
        }

        private DothtmlControlCollection GetControls<TControl>() where TControl : DotvvmControl
        {
            return new DothtmlControlCollection(Node.Content
                .Where(c => c.Metadata.Type == typeof(TControl))
                .Select(c => new DothtmlControl(c, validate)));
        }

        private DothtmlControlCollection GetDothtmlControls(Func<DothtmlControlCollection> activeAction)
        {
            return IsActive ? activeAction() : DothtmlControlCollection.Inactive;
        }

        private DothtmlControlCollection GetElements(string elementTag)
        {
            return new DothtmlControlCollection(Node.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Where(c => ((DothtmlElementNode)c.DothtmlNode).TagName == elementTag)
                .Select(c => new DothtmlControl(c, validate)));
        }

        private string TokensToString(IEnumerable<DothtmlToken> tokens)
        {
            var builder = new StringBuilder();
            foreach (var token in tokens)
            {
                builder.Append(token.Text);
            }
            return builder.ToString();
        }
    }
}