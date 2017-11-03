using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    using ImmutableMetadataDictionary = ImmutableDictionary<string, object>;

    public class ValidationAnalyzerContext
    {
        private ImmutableMetadataDictionary metadataStorage;

        public static ValidationAnalyzerContext Merge(IEnumerable<ValidationAnalyzerContext> contexts)
        {
            var mergedContext = new ValidationAnalyzerContext();
            var metadataStorageBuilder = ImmutableDictionary.CreateBuilder<string, object>();
            foreach (var context in contexts)
            {
                metadataStorageBuilder.AddRange(context.metadataStorage);
            }
            return mergedContext;
        }

        public void AddMetadata<TMetadata>(ImmutableDictionary<string, TMetadata> metadata)
            where TMetadata : IValidationAnalyzerMetadata
        {
            var fullName = typeof(TMetadata).FullName;
            if (metadataStorage.ContainsKey(fullName))
            {
                throw new InvalidOperationException($"The Context already contains metadata of type '{fullName}'.");
            }
            metadataStorage = metadataStorage.Add(fullName, metadata);
        }

        public ImmutableDictionary<string, TMetadata> GetMetadata<TMetadata>()
                    where TMetadata : IValidationAnalyzerMetadata
        {
            return metadataStorage[typeof(TMetadata).FullName] as ImmutableDictionary<string, TMetadata>;
        }
    }
}