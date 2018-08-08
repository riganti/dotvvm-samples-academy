using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlUnit : Validation.Unit.Unit
    {
        public DothtmlUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IQuery<ValidationControl> GetControls(string xpath)
            => GetQuery<ValidationControl>(xpath);

        public IQuery<ValidationDirective> GetDirectives(string xpath)
            => GetQuery<ValidationDirective>(xpath);

        public IQuery<ValidationPropertySetter> GetProperties(string xpath)
            => GetQuery<ValidationPropertySetter>(xpath);
    }
}