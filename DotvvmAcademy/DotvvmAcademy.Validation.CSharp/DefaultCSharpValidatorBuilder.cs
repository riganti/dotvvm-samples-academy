using DotvvmAcademy.Validation.Abstractions;
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

        public virtual ICSharpValidator Build()
        {
            var collection = new ServiceCollection();
            // add default implementations
            collection.AddDefaultCSharpValidation();
            // let the user override the default implementations
            ConfigureServices?.Invoke(collection);
            var provider = collection.BuildServiceProvider();
            var locatedMethods = ProcessAssemblies(provider);
            methods.AddRange(locatedMethods);
            var staticContexts = ProcessMethods(provider);
            var validator = provider.GetRequiredService<ICSharpValidator>();
            validator.StaticAnalysisContexts = staticContexts;
            return validator;
        }

        public virtual void RegisterAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public virtual void RegisterValidationMethod(MethodInfo method)
        {
            methods.Add(method);
        }

        protected virtual IEnumerable<MethodInfo> ProcessAssemblies(IServiceProvider provider)
        {
            if (assemblies.Count == 0)
            {
                return Enumerable.Empty<MethodInfo>();
            }
            var locator = provider.GetRequiredService<IValidationMethodLocator>();
            return assemblies.SelectMany(a => locator.LocateMethods(a));
        }

        protected ImmutableDictionary<string, ValidationAnalyzerContext> ProcessMethods(IServiceProvider provider)
        {
            var contexts = new Dictionary<string, ValidationAnalyzerContext>();
            var groupedMethods = methods.GroupBy(m => m.DeclaringType);
            foreach (var group in groupedMethods)
            {
                var instance = Activator.CreateInstance(group.Key);
                foreach (var method in group)
                {
                    var name = GetValidationMethodName(method, provider);
                    using (var scope = provider.CreateScope())
                    {
                        var factory = scope.ServiceProvider.GetRequiredService<ICSharpFactory>();
                        method.Invoke(instance, new[] { factory.GetDocument() });
                        contexts.Add(name, factory.GetValidationAnalyzerContext());
                    }
                }
            }
            return contexts.ToImmutableDictionary();
        }

        private string GetValidationMethodName(MethodInfo method, IServiceProvider provider)
        {
            var resolvers = provider.GetRequiredService<IEnumerable<IValidationMethodNameResolver>>();
            foreach (var resolver in resolvers)
            {
                if(resolver.TryResolveName(method, out var name))
                {
                    return name;
                }
            }
            throw new ArgumentException($"The name of the ValidationMethod '{method.ToString()}' could not be determined.");
        }
    }
}