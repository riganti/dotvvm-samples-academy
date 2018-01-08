using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public interface IValidationMethodNameResolver
    {
        bool TryResolveName(MethodInfo method, out string name);
    }
}