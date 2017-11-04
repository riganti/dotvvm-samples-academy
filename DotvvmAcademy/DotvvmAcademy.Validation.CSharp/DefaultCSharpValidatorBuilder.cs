using DotvvmAcademy.Validation.CSharp.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
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
        private List<MethodInfo> methods = new List<MethodInfo>();

        public Action<IServiceCollection> ConfigureServices { get; set; }

        public ICSharpValidator Build()
        {
            var collection = new ServiceCollection();
            // add default implementations
            collection.AddDefaultCSharpValidation();
            // let the user override the default implementations
            ConfigureServices?.Invoke(collection);
            var provider = collection.BuildServiceProvider();
            ProcessAssemblies();
            var staticContexts = ProcessMethods(provider);
            var validator = provider.GetRequiredService<ICSharpValidator>();
            validator.StaticAnalysisContexts = staticContexts;
            return validator;
        }

        public void RegisterAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public void RegisterValidationMethod(MethodInfo method)
        {
            methods.Add(method);
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
                methods.AddRange(assemblyMethods);
            }
        }

        protected ImmutableDictionary<string, CSharpStaticAnalysisContext> ProcessMethods(IServiceProvider provider)
        {
            var contexts = ImmutableDictionary.CreateBuilder<string, CSharpStaticAnalysisContext>();
            var groupedMethods = methods.GroupBy(m => m.DeclaringType);
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
                        var csharpObjects = factory.GetAllObjects();
                    }
                }
            }
            return contexts.ToImmutable();
        }
    }
}