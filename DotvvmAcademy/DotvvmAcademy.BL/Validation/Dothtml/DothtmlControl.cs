using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControl : DothtmlObject<ResolvedControl>
    {
        internal DothtmlControl(DothtmlValidate validate, ResolvedControl node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static DothtmlControl Inactive => new DothtmlControl(null, null, false);

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

        public void CommandBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a command binding on this control's '{property.Name}' property.";
            Binding<CommandBindingExpression>(property, message, expectedValues);
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
            if (!expectedValues.Contains(value.Value))
            {
                AddError($"This property contains an unexpected value.",
                    value.DothtmlNode.StartPosition, value.DothtmlNode.EndPosition);
            }
        }

        public void ResourceBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a resource binding on this control's '{property.Name}' property.";
            Binding<ResourceBindingExpression>(property, message, expectedValues);
        }

        public void StaticCommandBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a static command binding on this control's '{property.Name}' property.";
            Binding<StaticCommandBindingExpression>(property, message, expectedValues);
        }

        public void ValueBinding(IPropertyDescriptor property, params string[] expectedValues)
        {
            var message = $"There should be a value binding on this control's '{property.Name}' property.";
            Binding<ValueBindingExpression>(property, message, expectedValues);
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

            if (binding.Binding.BindingType != typeof(TBinding))
            {
                AddError(wrongTypeErrorMessage, binding.DothtmlNode.StartPosition, binding.DothtmlNode.EndPosition);
            }

            if (!expectedValues.Contains(binding.Binding.Value.Trim()))
            {
                AddError($"This binding contains an incorrect value.",
                    binding.Binding.BindingNode.StartPosition, binding.Binding.BindingNode.EndPosition);
            }
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