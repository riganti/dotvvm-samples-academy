using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationMethodLocator
    {
        IEnumerable<MethodInfo> LocateMethods(Assembly assembly);
    }
}