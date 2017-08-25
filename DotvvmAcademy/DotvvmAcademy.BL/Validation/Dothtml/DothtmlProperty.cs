using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Linq;
using System.Collections.Immutable;


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

        public void CommandBinding(params string[] expectedValues) => Binding<CommandBindingExpression>(expectedValues);

        public void ControlCommandBinding(params string[] expectedValues) => Binding<ControlCommandBindingExpression>(expectedValues);

        public void ControlPropertiesBinding(params string[] expectedValues) => Binding<ControlPropertyBindingExpression>(expectedValues);

        public void HardcodedValue(params object[] expectedValues)
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

        public DothtmlControlCollection TemplateContent()
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;
            if(!(Setter is ResolvedPropertyTemplate template))
            {
                AddError("This property should contain a template.");
                return DothtmlControlCollection.Inactive;
            }

            return new DothtmlControlCollection(Validate, template.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Select(c => new DothtmlControl(Validate, c))
                .ToImmutableList(), template.Parent);
        }

        public void ResourceBinding(params string[] expectedValues) => Binding<ResourceBindingExpression>(expectedValues);

        public void StaticCommandBinding(params string[] expectedValues) => Binding<StaticCommandBindingExpression>(expectedValues);

        public void ValueBinding(params string[] expectedValues) => Binding<ValueBindingExpression>(expectedValues);

        public override void AddError(string message) => AddError(message, Setter.DothtmlNode.StartPosition, Setter.DothtmlNode.EndPosition);

        private void Binding<TBinding>(params string[] expectedValues) where TBinding : BindingExpression
        {
            if (!IsActive) return;

            if (Setter is ResolvedPropertyBinding binding && binding.Binding.BindingType == typeof(TBinding))
            {
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