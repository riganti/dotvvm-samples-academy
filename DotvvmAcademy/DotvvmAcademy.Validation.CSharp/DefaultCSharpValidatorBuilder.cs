using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpValidatorBuilder : ICSharpValidatorBuilder
    {
        private List<Assembly> assemblies = new List<Assembly>();
        private Action<IServiceCollection> configureCollection;
        private List<MethodInfo> methodInfos = new List<MethodInfo>();
        private Dictionary<string, CSharpValidationMethod> methods = new Dictionary<string, CSharpValidationMethod>();

        public void AddValidationAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public void AddValidationMethod(string name, CSharpValidationMethod method)
        {
            methods.Add(name, method);
        }

        public void AddValidationMethod(MethodInfo method)
        {
            methodInfos.Add(method);
        }

        public ICSharpValidator Build()
        {
            var collection = new ServiceCollection();
            collection.AddCSharpValidationInternalServices();
            configureCollection?.Invoke(collection);
            var provider = collection.BuildServiceProvider();
            ProcessAssemblies();
            ProcessMethods(provider);
            var validator = new DefaultCSharpValidator(methods.ToImmutableDictionary(), provider);
            return validator;
        }

        public void ConfigureServiceCollection(Action<IServiceCollection> configureCollection)
        {
            this.configureCollection = configureCollection;
        }

        protected virtual string GetValidationMethodName(MethodInfo info)
        {
            var attribute = info.GetCustomAttribute<ValidationMethodAttribute>();
            return attribute.Name ?? info.Name;
        }

        protected virtual void ProcessAssemblies()
        {
            foreach (var assembly in assemblies)
            {
                var assemblyMethods = assembly.GetTypes()
                    .Where(t => t.GetCustomAttribute<ValidationClassAttribute>() != null)
                    .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    .Where(m => m.GetCustomAttribute<ValidationMethodAttribute>() != null);
                methodInfos.AddRange(assemblyMethods);
            }
        }

        protected virtual void ProcessMethods(IServiceProvider provider)
        {
            var groupedMethods = methodInfos.GroupBy(m => m.DeclaringType);
            foreach (var group in groupedMethods)
            {
                var instance = Activator.CreateInstance(group.Key);
                foreach (var method in group)
                {
                    var name = GetValidationMethodName(method);
                    using (var scope = provider.CreateScope())
                    {
                        var factory = scope.ServiceProvider.GetRequiredService<ICSharpFactory>();
                        method.Invoke(instance, new[] { factory.GetDocument() });
                        methods.Add(name, factory.CreateValidationMethod());
                    }
                }
            }
        }
    }
}