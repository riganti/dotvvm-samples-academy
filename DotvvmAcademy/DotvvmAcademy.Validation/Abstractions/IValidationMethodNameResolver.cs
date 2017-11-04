using System.Reflection;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationMethodNameResolver
    {
        bool TryResolveName(MethodInfo method, out string name);
    }
}