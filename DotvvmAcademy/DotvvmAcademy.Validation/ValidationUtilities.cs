using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public static class ValidationUtilities
    {
        public static IDictionary<string, MethodInfo> GetValidationMethods<TDocument>(Assembly assembly)
            where TDocument : IDocument
        {
            var locator = new DefaultValidationMethodLocator();
            var methods = locator.GetMethods<TDocument>(assembly);
            var nameResolvers = new IValidationMethodNameResolver[]
            {
                new AttributeValidationMethodNameResolver(),
                new MethodNameValidationMethodNameResolver()
            };
            return methods.ToImmutableDictionary(m => ResolveName(m, nameResolvers));
        }

        private static string ResolveName(MethodInfo method, IEnumerable<IValidationMethodNameResolver> nameResolvers)
        {
            foreach (var resolver in nameResolvers)
            {
                if (resolver.TryResolveName(method, out string name))
                {
                    return name;
                }
            }
            throw new ArgumentException("The validation method name couldn't be inferred.")
        }
    }
}