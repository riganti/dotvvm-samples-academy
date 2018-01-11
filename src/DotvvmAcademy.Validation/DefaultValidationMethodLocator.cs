using DotvvmAcademy.Validation.Abstractions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public class DefaultValidationMethodLocator : IValidationMethodLocator
    {
        public IEnumerable<MethodInfo> GetMethods(Assembly assembly) => GetMethods<IDocument>(assembly);

        public IEnumerable<MethodInfo> GetMethods<TDocument>(Assembly assembly)
            where TDocument : IDocument
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ValidationClassAttribute>() != null)
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                .Where(IsValidationMethod<TDocument>)
                .ToImmutableArray();
        }

        private bool IsValidationMethod<TDocument>(MethodInfo method)
            where TDocument : IDocument
        {
            if(method.GetCustomAttribute<ValidationMethodAttribute>() == null)
            {
                return false;
            }

            var parameters = method.GetParameters();
            if(parameters.Length != 1)
            {
                return false;
            }

            var parameterType = parameters[0].ParameterType;
            if(!typeof(TDocument).IsAssignableFrom(parameterType))
            {
                return false;
            }

            return true;
        }
    }
}