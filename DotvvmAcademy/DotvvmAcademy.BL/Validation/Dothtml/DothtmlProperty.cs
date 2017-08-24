using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
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

        public void CommandBinding(params string[] expectedValues) => Binding<CommandBindingExpression>(expectedValues);

        public void ControlCommandBinding(params string[] expectedValues) => Binding<ControlCommandBindingExpression>(expectedValues);

        public void ControlPropertiesBinding(params string[] expectedValues) => Binding<ControlPropertyBindingExpression>(expectedValues);

        public void HardcodedValue(params object[] expectedValues)
        {
            if (!IsActive) return;

            if (!(Setter is ResolvedPropertyValue))
            {
                AddError("This property should contain a hard-coded value.");
                return;
            }

            var value = ((ResolvedPropertyValue)Setter).Value;
            if (!expectedValues.Contains(value))
            {
                AddError("This property contains an incorrect hard-coded value.");
            }
        }

        public void ResourceBinding(params string[] expectedValues) => Binding<ResourceBindingExpression>(expectedValues);

        public void StaticCommandBinding(params string[] expectedValues) => Binding<StaticCommandBindingExpression>(expectedValues);

        public void ValueBinding(params string[] expectedValues) => Binding<ValueBindingExpression>(expectedValues);

        protected override void AddError(string message) => AddError(message, Setter.DothtmlNode.StartPosition, Setter.DothtmlNode.EndPosition);

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