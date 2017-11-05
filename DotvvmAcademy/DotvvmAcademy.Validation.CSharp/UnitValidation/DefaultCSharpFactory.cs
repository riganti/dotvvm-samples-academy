using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly ImmutableDictionary<string, ICSharpObject>.Builder cache = ImmutableDictionary.CreateBuilder<string, ICSharpObject>();
        private readonly IServiceProvider provider;

        public DefaultCSharpFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ImmutableDictionary<string, ICSharpObject> GetAllObjects()
        {
            return cache.ToImmutable();
        }

        public ICSharpDocument GetDocument()
        {
            return provider.GetRequiredService<ICSharpDocument>();
        }

        public TCSharpObject GetObject<TCSharpObject>(string fullName) where TCSharpObject : ICSharpObject
        {
            cache.TryGetValue(fullName, out var value);
            if (value == null)
            {
                value = provider.GetRequiredService<TCSharpObject>();
                value.SetUniqueFullName(fullName);
            }

            if (value is TCSharpObject castObject)
            {
                return castObject;
            }
            else
            {
                throw new InvalidOperationException($"No '{typeof(TCSharpObject).Name}' with full name '{fullName}' has been found. " +
                    $"The cache already contains a '{value.GetType().Name}' with the same name.");
            }
        }

        public void RemoveObject<TCSharpObject>(TCSharpObject csharpObject) where TCSharpObject : ICSharpObject
        {
            var values = cache.Where((k, v) => ReferenceEquals(v, csharpObject));
            foreach (var pair in values)
            {
                cache.Remove(pair.Key);
            }
        }
    }
}