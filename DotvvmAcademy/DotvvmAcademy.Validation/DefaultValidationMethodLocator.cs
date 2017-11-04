using DotvvmAcademy.Validation.Abstractions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public class DefaultValidationMethodLocator : IValidationMethodLocator
    {
        public IEnumerable<MethodInfo> LocateMethods(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ValidationClassAttribute>() != null)
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                .Where(m => m.GetCustomAttribute<ValidationMethodAttribute>() != null)
                .ToImmutableArray();
        }
    }
}