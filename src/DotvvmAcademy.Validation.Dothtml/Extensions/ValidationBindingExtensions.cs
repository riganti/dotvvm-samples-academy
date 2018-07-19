using DotVVM.Framework.Binding.Expressions;
using DotvvmAcademy.Validation.Dothtml.Unit;
using System;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public static class ValidationBindingExtensions
    {
        public static BindingKind GetBindingKind(this ValidationBinding binding)
        {
            if (binding.BindingType == typeof(ValueBindingExpression))
            {
                return BindingKind.Value;
            }
            else if (binding.BindingType == typeof(CommandBindingExpression))
            {
                return BindingKind.Command;
            }
            else if (binding.BindingType == typeof(StaticCommandBindingExpression))
            {
                return BindingKind.StaticCommand;
            }
            else if (binding.BindingType == typeof(ResourceBindingExpression))
            {
                return BindingKind.Resource;
            }
            else if (binding.BindingType == typeof(ControlPropertyBindingExpression))
            {
                return BindingKind.ControlProperty;
            }
            else if (binding.BindingType == typeof(ControlCommandBindingExpression))
            {
                return BindingKind.ControlCommand;
            }

            throw new NotSupportedException($"Binding type '{binding.GetType()}' is not supported.");
        }
    }
}