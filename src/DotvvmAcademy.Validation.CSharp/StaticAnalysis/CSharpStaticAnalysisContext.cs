using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class CSharpStaticAnalysisContext
    {
        public static CSharpStaticAnalysisContext Default = GetDefaultStaticAnalysisContext();

        private ConcurrentDictionary<Type, StaticAnalysisMetadataCollection> storage
            = new ConcurrentDictionary<Type, StaticAnalysisMetadataCollection>();

        public void AddMetadata(Type analyzerType, StaticAnalysisMetadataCollection metadata)
        {
            storage.AddOrUpdate(analyzerType, metadata, (key, value) => value + metadata);
        }

        public StaticAnalysisMetadataCollection GetMetadata(Type analyzerType)
        {
            storage.TryGetValue(analyzerType, out var value);
            return value;
        }

        private static CSharpStaticAnalysisContext GetDefaultStaticAnalysisContext()
        {
            var allowedSymbols = ImmutableHashSet.Create(
                "global::System.Boolean",
                "global::System.Byte",
                "global::System.SByte",
                "global::System.Char",
                "global::System.Decimal",
                "global::System.Double",
                "global::System.Single",
                "global::System.Int32",
                "global::System.UInt32",
                "global::System.Int64",
                "global::System.UInt64",
                "global::System.Object",
                "global::System.Int16",
                "global::System.UInt16",
                "global::System.String",
                "global::System.Void",
                "global::System.Object.ToString()",
                "global::System.Object.Equals(object)",
                "global::System.Object.ReferenceEquals(object, object)",
                "global::System.Object.GetHashCode()");
            var context = new CSharpStaticAnalysisContext();
            context.AddMetadata<AllowedSymbolAnalyzer>(allowedSymbols);
            return context;
        }
    }
}