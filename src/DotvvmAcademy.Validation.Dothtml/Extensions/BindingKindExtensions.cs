using DotVVM.Framework.Binding.Expressions;
using System;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class BindingKindExtensions
    {
        public static Type GetBindingType(this BindingKind kind)
        {
            switch (kind)
            {
                case BindingKind.Value:
                    return typeof(ValueBindingExpression<>);

                case BindingKind.Command:
                    return typeof(CommandBindingExpression<>);

                case BindingKind.Resource:
                    return typeof(ResourceBindingExpression<>);

                case BindingKind.StaticCommand:
                    return typeof(StaticCommandBindingExpression<>);

                case BindingKind.ControlProperty:
                    return typeof(ControlPropertyBindingExpression<>);

                case BindingKind.ControlCommand:
                    return typeof(ControlCommandBindingExpression<>);

                default:
                    throw new NotSupportedException($"{nameof(BindingKind)} '{kind}' is not supported.");
            }
        }
    }
}