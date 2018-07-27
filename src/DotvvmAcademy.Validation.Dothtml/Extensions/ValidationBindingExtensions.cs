using DotVVM.Framework.Binding.Expressions;
using DotvvmAcademy.Validation.Dothtml.Unit;
using System;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public static class ValidationBindingExtensions
    {
        public static BindingKind GetBindingKind(this ValidationBinding binding)
        {
            if (typeof(ValueBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.Value;
            }
            else if (typeof(CommandBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.Command;
            }
            else if (typeof(StaticCommandBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.StaticCommand;
            }
            else if (typeof(ResourceBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.Resource;
            }
            else if (typeof(ControlPropertyBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.ControlProperty;
            }
            else if (typeof(ControlCommandBindingExpression).IsAssignableFrom(binding.BindingType))
            {
                return BindingKind.ControlCommand;
            }

            throw new NotSupportedException($"Binding type '{binding.BindingType}' is not supported.");
        }
    }
}