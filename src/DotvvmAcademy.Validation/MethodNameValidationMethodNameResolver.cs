using DotvvmAcademy.Validation.Abstractions;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public class MethodNameValidationMethodNameResolver : IValidationMethodNameResolver
    {
        public bool TryResolveName(MethodInfo method, out string name)
        {
            name = method.Name;
            return true;
        }
    }
}