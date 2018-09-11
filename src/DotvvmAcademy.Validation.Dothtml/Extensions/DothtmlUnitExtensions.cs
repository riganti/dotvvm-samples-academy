using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlUnitExtensions
    {
        public static DothtmlQuery<ValidationControl> GetControl(this DothtmlUnit unit, string xpath)
        {
            return unit.GetControls(xpath).CountEquals(1);
        }

        public static DothtmlQuery<ValidationControl> GetControls(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationControl>(xpath);
        }

        public static DothtmlQuery<ValidationDirective> GetDirective(this DothtmlUnit unit, string xpath)
        {
            return unit.GetDirectives(xpath).CountEquals(1);
        }

        public static DothtmlQuery<ValidationDirective> GetDirectives(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationDirective>(xpath);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperties(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationPropertySetter>(xpath);
        }

        public static DothtmlQuery<ValidationPropertySetter> GetProperty(this DothtmlUnit unit, string xpath)
        {
            return unit.GetProperties(xpath).CountEquals(1);
        }

        public static DothtmlQuery<TResult> GetQuery<TResult>(this DothtmlUnit unit, string xpath)
            where TResult : ValidationTreeNode
        {
            return new DothtmlQuery<TResult>(unit, XPathExpression.Compile(xpath));
        }
    }
}