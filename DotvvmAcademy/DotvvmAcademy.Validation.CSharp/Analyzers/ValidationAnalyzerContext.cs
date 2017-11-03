using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public class ValidationAnalyzerContext
    {
        private ConcurrentDictionary<string, object> data =
            new ConcurrentDictionary<string, object>();

        public void AddMetadata<TMetadata>(ImmutableDictionary<string, TMetadata> metadata)
            where TMetadata : IValidationAnalyzerMetadata
        {
            data.GetOrAdd(typeof(TMetadata).FullName, metadata);
        }

        public ImmutableDictionary<string, TMetadata> GetMetadata<TMetadata>()
                    where TMetadata : IValidationAnalyzerMetadata
        {
            return data[typeof(TMetadata).FullName] as ImmutableDictionary<string, TMetadata>;
        }
    }
}