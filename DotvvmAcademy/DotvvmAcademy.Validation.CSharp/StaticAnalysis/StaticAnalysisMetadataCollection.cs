using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class StaticAnalysisMetadataCollection
    {
        public StaticAnalysisMetadataCollection(ImmutableDictionary<string, IStaticAnalysisMetadata> pairs)
        {
            Pairs = pairs ?? throw new System.ArgumentNullException(nameof(pairs));
        }

        public StaticAnalysisMetadataCollection(ImmutableHashSet<string> fullNames)
        {
            FullNames = fullNames;
        }

        public StaticAnalysisMetadataCollection(ImmutableHashSet<IStaticAnalysisMetadata> objects)
        {
            Objects = objects;
        }

        public ImmutableHashSet<string> FullNames { get; }

        public ImmutableHashSet<IStaticAnalysisMetadata> Objects { get; }

        public ImmutableDictionary<string, IStaticAnalysisMetadata> Pairs { get; }

        public IStaticAnalysisMetadata this[string symbolNameKey] => Pairs[symbolNameKey];

        public static implicit operator StaticAnalysisMetadataCollection(ImmutableHashSet<string> fullNames)
        {
            return new StaticAnalysisMetadataCollection(fullNames);
        }

        public static implicit operator StaticAnalysisMetadataCollection(ImmutableHashSet<IStaticAnalysisMetadata> objects)
        {
            return new StaticAnalysisMetadataCollection(objects);
        }

        public static implicit operator StaticAnalysisMetadataCollection(ImmutableDictionary<string, IStaticAnalysisMetadata> pairs)
        {
            return new StaticAnalysisMetadataCollection(pairs);
        }

        public static StaticAnalysisMetadataCollection operator +(StaticAnalysisMetadataCollection first, StaticAnalysisMetadataCollection second)
        {
            if (first.FullNames != null && second.FullNames != null)
            {
                var fullNamesBuilder = ImmutableHashSet.CreateBuilder<string>();
                fullNamesBuilder.UnionWith(first.FullNames);
                fullNamesBuilder.UnionWith(second.FullNames);
                return fullNamesBuilder.ToImmutable();
            }

            if (first.Objects != null && second.Objects != null)
            {
                var objectsBuilder = ImmutableHashSet.CreateBuilder<IStaticAnalysisMetadata>();
                objectsBuilder.UnionWith(first.Objects);
                objectsBuilder.UnionWith(second.Objects);
                return objectsBuilder.ToImmutable();
            }

            if (first.Pairs != null && second.Pairs != null)
            {
                var builder = ImmutableDictionary.CreateBuilder<string, IStaticAnalysisMetadata>();
                builder.AddRange(second.Pairs);
                foreach (var existingPair in first.Pairs)
                {
                    if (!builder.ContainsKey(existingPair.Key))
                    {
                        builder.Add(existingPair);
                    }
                }
                return builder.ToImmutable();
            }

            throw new ArgumentException($"Cannot add two {nameof(StaticAnalysisMetadataCollection)}s that contain data in different properties.");
        }
    }
}