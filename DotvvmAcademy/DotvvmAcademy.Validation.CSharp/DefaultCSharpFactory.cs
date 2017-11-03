using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using DotvvmAcademy.Validation.CSharp.Analyzers;
using System.Collections;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly ConcurrentDictionary<string, ICSharpObject> cache = new ConcurrentDictionary<string, ICSharpObject>();
        private readonly IServiceProvider provider;
        private readonly IEnumerable<IMetadataExtractor> extractors;

        public DefaultCSharpFactory(IServiceProvider provider, IEnumerable<IMetadataExtractor> extractors)
        {
            this.provider = provider;
            this.extractors = extractors;
        }

        public ICSharpDocument GetDocument()
        {
            return provider.GetRequiredService<ICSharpDocument>();
        }

        public TCSharpObject GetObject<TCSharpObject>(string fullName) where TCSharpObject : ICSharpObject
        {
            var value = cache.GetOrAdd(fullName, s =>
            {
                var service = provider.GetRequiredService<TCSharpObject>();
                service.SetUniqueFullName(s);
                return service;
            });
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

        public ValidationAnalyzerContext GetValidationAnalyzerContext()
        {
            var immutableCache = cache.ToImmutableDictionary();
            var metadata = Immutable
            var context = new ValidationAnalyzerContext();
            foreach (var extractor in extractors)
            {
                context.AddMetadata(extractor.ExtractMetadata(immutableCache));
            }
            return context;
        }

        public void RemoveObject<TCSharpObject>(TCSharpObject csharpObject) where TCSharpObject : ICSharpObject
        {
            var values = cache.Where((k, v) => ReferenceEquals(v, csharpObject));
            foreach (var pair in values)
            {
                cache.TryRemove(pair.Key, out _);
            }
        }
    }
}