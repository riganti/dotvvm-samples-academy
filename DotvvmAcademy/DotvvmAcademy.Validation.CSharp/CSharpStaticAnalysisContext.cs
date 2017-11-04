﻿using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpStaticAnalysisContext
    {
        private Dictionary<Type, object> metadataStorage = new Dictionary<Type, object>();

        public ImmutableArray<CSharpSyntaxTree> ValidatedTrees { get; set; }

        public static CSharpStaticAnalysisContext Merge(IEnumerable<CSharpStaticAnalysisContext> contexts)
        {
            var validatedTreesBuilder = ImmutableArray.CreateBuilder<CSharpSyntaxTree>();
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