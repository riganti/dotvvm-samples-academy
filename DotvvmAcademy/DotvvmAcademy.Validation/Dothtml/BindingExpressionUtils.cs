using DotVVM.Framework.Binding.Expressions;

namespace DotvvmAcademy.Validation.Dothtml
{
    public static class BindingExpressionUtils
    {
        public static string GetHumanReadable<TBinding>()
            where TBinding : BindingExpression
        {
            var type = typeof(TBinding);
            if (type == typeof(ValueBindingExpression))
            {
                return "value binding";
            }
            else if (type == typeof(CommandBindingExpression))
            {
                return "command binding";
            }
            else if (type == typeof(StaticCommandBindingExpression))
            {
                return "static command binding";
            }
            else if (type == typeof(ResourceBindingExpression))
            {
                return "resource binding";
            }
            else if (type == typeof(ControlCommandBindingExpression))
            {
                return "control command binding";
            }
            else if (type == typeof(ControlPropertyBindingExpression))
            {
                return "control property binding";
            }

            return "unknown binding";
        }
    }
}