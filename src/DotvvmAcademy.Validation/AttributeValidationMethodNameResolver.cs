using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public class AttributeValidationMethodNameResolver : IValidationMethodNameResolver
    {
        public bool TryResolveName(MethodInfo method, out string name)
        {
            var attribute = method.GetCustomAttribute<ValidationMethodAttribute>();
            name = attribute?.Name;
            return name != null;
        }
    }
}