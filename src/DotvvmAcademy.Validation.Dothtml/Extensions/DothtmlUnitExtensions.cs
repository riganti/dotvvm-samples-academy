using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlUnitExtensions
    {
        public static IQuery<ValidationControl> GetControl(this DothtmlUnit unit, string xpath)
        {
            return unit.GetControls(xpath).CountEquals(1);
        }

        public static IQuery<ValidationControl> GetControls(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationControl>(xpath);
        }

        public static IQuery<ValidationDirective> GetDirective(this DothtmlUnit unit, string xpath)
        {
            return unit.GetDirectives(xpath).CountEquals(1);
        }

        public static IQuery<ValidationDirective> GetDirectives(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationDirective>(xpath);
        }

        public static IQuery<ValidationPropertySetter> GetProperties(this DothtmlUnit unit, string xpath)
        {
            return unit.GetQuery<ValidationPropertySetter>(xpath);
        }

        public static IQuery<ValidationPropertySetter> GetProperty(this DothtmlUnit unit, string xpath)
        {
            return unit.GetProperties(xpath).CountEquals(1);
        }
    }
}