using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly ImmutableDictionary<string, ICSharpObject>.Builder objects = ImmutableDictionary.CreateBuilder<string, ICSharpObject>();
        private readonly IServiceProvider provider;
        private readonly ICSharpNameStack nameStack;

        public DefaultCSharpFactory(IServiceProvider provider)
        {
            this.provider = provider;
            nameStack = provider.GetRequiredService<ICSharpNameStack>();
        }

        public virtual ImmutableDictionary<string, ICSharpObject> GetAllObjects()
        {
            return objects.ToImmutable();
        }

        public virtual ICSharpDocument GetDocument()
        {
            return provider.GetRequiredService<ICSharpDocument>();
        }

        public virtual TCSharpObject GetObject<TCSharpObject>(string fullName) where TCSharpObject : ICSharpObject
        {
            objects.TryGetValue(fullName, out var value);
            if (value == null)
            {
                nameStack.PushName(fullName);
                return provider.GetRequiredService<TCSharpObject>();
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
            var values = objects.Where((k, v) => ReferenceEquals(v, csharpObject));
            foreach (var pair in values)
            {
                objects.Remove(pair.Key);
            }
        }
    }
}