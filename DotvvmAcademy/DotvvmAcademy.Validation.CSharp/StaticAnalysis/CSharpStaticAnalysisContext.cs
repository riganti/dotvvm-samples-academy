using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class CSharpStaticAnalysisContext
    {
        private ConcurrentDictionary<Type, object> metadataStorage = new ConcurrentDictionary<Type, object>();

        public ImmutableArray<CSharpSyntaxTree> ValidatedTrees { get; set; }

        public void AddMetadata<TMetadata>(ImmutableDictionary<string, TMetadata> metadata)
            where TMetadata : IStaticAnalysisMetadata
        {
            var type = typeof(TMetadata);
            metadataStorage.AddOrUpdate(type, metadata, (key, value) =>
            {
                var existingMetadata = (ImmutableDictionary<string, TMetadata>)value;
                var builder = ImmutableDictionary.CreateBuilder<string, TMetadata>();
                builder.AddRange(metadata);
                foreach (var existingPair in existingMetadata)
                {
                    if (!builder.ContainsKey(existingPair.Key))
                    {
                        builder.Add(existingPair);
                    }
                }
                return builder.ToImmutable();
            });
        }

        public ImmutableDictionary<string, TMetadata> GetMetadata<TMetadata>()
            where TMetadata : IStaticAnalysisMetadata
        {
            metadataStorage.TryGetValue(typeof(TMetadata), out var value);
            return (ImmutableDictionary<string, TMetadata>)value ?? null;
        }
    }
}