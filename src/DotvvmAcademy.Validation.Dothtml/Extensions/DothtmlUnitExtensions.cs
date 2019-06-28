using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlUnitExtensions
    {
        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlUnit unit, string expression)
        {
            return unit.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<ValidationControl>(expression);
        }

        public static DothtmlQuery<ValidationDirective> GetDirective(this DothtmlUnit unit, string expression)
        {
            return unit.GetDirectives(expression).RequireCount(1);
        }

        public static DothtmlQuery<ValidationDirective> GetDirectives(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<ValidationDirective>(expression);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperties(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<ValidationPropertySetter>(expression);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperty(this DothtmlUnit unit, string expression)
        {
            return unit.GetProperties(expression).RequireCount(1);
        }

        public static DothtmlQuery<TResult> GetQuery<TResult>(this DothtmlUnit unit, string expression)
            where TResult : ValidationTreeNode
        {
            return new DothtmlQuery<TResult>(unit, XPathExpression.Compile(expression));
        }
    }
}