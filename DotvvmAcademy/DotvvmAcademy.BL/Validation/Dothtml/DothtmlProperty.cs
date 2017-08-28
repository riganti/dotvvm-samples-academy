using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlProperty : ValidationObject<DothtmlValidate>
    {
        internal DothtmlProperty(DothtmlValidate validate, IAbstractPropertySetter setter, bool isActive = true) : base(validate, isActive)
        {
            Setter = setter;
        }

        public static DothtmlProperty Inactive { get; } = new DothtmlProperty(null, null, false);

        public IAbstractPropertySetter Setter { get; }

        public override void AddError(string message)
        {
            if (Setter is ResolvedPropertyBinding binding)
            {
                AddError(message, binding.Binding.DothtmlNode.StartPosition, binding.Binding.DothtmlNode.EndPosition);
            }
            else if (Setter.DothtmlNode != null)
            {
                AddError(message, Setter.DothtmlNode.StartPosition, Setter.DothtmlNode.EndPosition);
            }
            else
            {
                IAbstractTreeNode current = Setter.Parent;
                while(current.Parent?.DothtmlNode == null)
                {
                    current = current.Parent;
                }
                AddError(message, current.DothtmlNode.StartPosition, current.DothtmlNode.EndPosition);
            }
        }

        public void CommandBinding(IEnumerable<string> expectedValues) => Binding<CommandBindingExpression>(expectedValues);

        public void ControlCommandBinding(IEnumerable<string> expectedValues) => Binding<ControlCommandBindingExpression>(expectedValues);

        public void ControlPropertyBinding(IEnumerable<string> expectedValues) => Binding<ControlPropertyBindingExpression>(expectedValues);

        public void HardcodedValue(IEnumerable<string> expectedValues)
        {
            if (!IsActive) return;

            if (!(Setter is ResolvedPropertyValue value))
            {
                AddError("This property should contain a hard-coded value.");
                return;
            }

            if (!expectedValues.Contains(value.Value))
            {
                AddError("This property contains an incorrect hard-coded value.");
            }
        }

        public void ResourceBinding(IEnumerable<string> expectedValues) => Binding<ResourceBindingExpression>(expectedValues);

        public void StaticCommandBinding(IEnumerable<string> expectedValues) => Binding<StaticCommandBindingExpression>(expectedValues);

        public DothtmlControlCollection TemplateContent()
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;
            if (!(Setter is ResolvedPropertyTemplate template))
            {
                AddError("This property should contain a template.");
                return DothtmlControlCollection.Inactive;
            }

            return new DothtmlControlCollection(Validate, template.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Select(c => new DothtmlControl(Validate, c))
                .ToImmutableList(), template.Parent);
        }

        public void ValueBinding(IEnumerable<string> expectedValues) => Binding<ValueBindingExpression>(expectedValues);

        private void Binding<TBinding>(IEnumerable<string> expectedValues) where TBinding : BindingExpression
        {
            if (!IsActive) return;

            if (Setter is ResolvedPropertyBinding binding && binding.Binding.BindingType == typeof(TBinding))
            {
                if (binding.Binding.ParsingError != null)
                {
                    AddError($"This binding is not valid. Parsing exception: '{binding.Binding.ParsingError.Message}'.");
                    return;
                }

                if (!expectedValues.Contains(binding.Binding.Value.Trim()))
                {
                    AddError($"This {BindingExpressionUtils.GetHumanReadable<TBinding>()} contains an incorrect value.");
                    return;
                }
                else
                {
                    return;
                }
            }

            AddError($"This property should contain a {BindingExpressionUtils.GetHumanReadable<TBinding>()}.");
        }
    }
}