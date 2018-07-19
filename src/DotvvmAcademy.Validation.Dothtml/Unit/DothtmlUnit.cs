using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlUnit : IValidationUnit
    {
        public string CorrectCode { get; set; }

        public string DefaultCode { get; set; }

        public ConcurrentDictionary<string, DothtmlQuery> Queries { get; }

        public DothtmlQuery<ValidationControl> GetControls(string xpath)
        {
            return (DothtmlQuery<ValidationControl>)Queries.GetOrAdd(xpath, x
                => new DothtmlQuery<ValidationControl>(xpath));
        }

        public DothtmlQuery<ValidationDirective> GetDirectives(string xpath)
        {
            return (DothtmlQuery<ValidationDirective>)Queries.GetOrAdd(xpath, x
                => new DothtmlQuery<ValidationDirective>(xpath));
        }

        public DothtmlQuery<ValidationPropertySetter> GetProperties(string xpath)
        {
            return (DothtmlQuery<ValidationPropertySetter>)Queries.GetOrAdd(xpath, x
                => new DothtmlQuery<ValidationPropertySetter>(xpath));
        }
    }
}