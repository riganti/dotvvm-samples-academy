using DotVVM.Framework.Compilation.ControlTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlUnitExtensions
    {
        public static DothtmlQuery<IAbstractControl> GetControl(this DothtmlUnit unit, string expression)
        {
            return unit.GetControls(expression).RequireCount(1);
        }

        public static DothtmlQuery<IAbstractControl> GetControls(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<IAbstractControl>(expression);
        }

        public static DothtmlQuery<IAbstractDirective> GetDirective(this DothtmlUnit unit, string expression)
        {
            return unit.GetDirectives(expression).RequireCount(1);
        }

        public static DothtmlQuery<IAbstractDirective> GetDirectives(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<IAbstractDirective>(expression);
        }

        public static DothtmlQuery<IAbstractPropertySetter> GetProperties(this DothtmlUnit unit, string expression)
        {
            return unit.GetQuery<IAbstractPropertySetter>(expression);
        }

        public static DothtmlQuery<IAbstractPropertySetter> GetProperty(this DothtmlUnit unit, string expression)
        {
            return unit.GetProperties(expression).RequireCount(1);
        }

        public static DothtmlQuery<TResult> GetQuery<TResult>(this DothtmlUnit unit, string expression)
            where TResult : IAbstractTreeNode
        {
            return new DothtmlQuery<TResult>(unit, XPathExpression.Compile(expression));
        }
    }
}
