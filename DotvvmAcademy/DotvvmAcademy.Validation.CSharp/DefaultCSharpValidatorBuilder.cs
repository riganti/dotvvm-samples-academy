using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidatorBuilder : ICSharpValidatorBuilder
    {
        private IServiceProvider provider;
        private List<Assembly> assemblies = new List<Assembly>();
        private List<MethodInfo> methods = new List<MethodInfo>();
        private Dictionary<string, CSharpValidationMethod> resolvedMethods = new Dictionary<string, CSharpValidationMethod>();

        public void AddValidationMethod(string name, CSharpValidationMethod method)
        {
            resolvedMethods.Add(name, method);
        }

        public void AddValidationMethod(MethodInfo method)
        {
            methods.Add(method);
        }

        public void AddValidationMethods(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public ICSharpValidator Build()
        {
            ProcessAssemblies();
            ProcessMethods();

            var validator = new DefaultCSharpValidator(resolvedMethods.ToImmutableDictionary());
            return validator;
        }

        public void SetServiceProvider(IServiceProvider provider)
        {
            this.provider = provider;
        }

        protected virtual void ProcessAssemblies()
        {
            foreach (var assembly in assemblies)
            {
                var assemblyMethods = assembly.GetTypes()
                    .Where(t => t.GetCustomAttribute<ValidationClassAttribute>() != null)
                    .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    .Where(m => m.GetCustomAttribute<ValidationMethodAttribute>() != null);
                methods.AddRange(assemblyMethods);
            }
        }

        protected virtual void ProcessMethods()
        {
            var groupedMethods = methods.GroupBy(m => m.DeclaringType);
            foreach (var group in groupedMethods)
            {
                var instance = Activator.CreateInstance(group.Key);
                foreach (var method in group)
                {
                    method.Invoke(instance, );
                }
            }
        }
    }
}