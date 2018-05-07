using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public abstract class ValidationDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        public ValidationDiagnosticAnalyzer(MetadataCollection<MetadataName> metadata)
        {
            Metadata = metadata;
        }

        public MetadataCollection<MetadataName> Metadata { get; }

        protected ImmutableArray<MetadataName> GetNamesWithProperty(string propertyKey)
        {
            return Metadata
                .Where(o => o.Value.Any(i => i.Key == propertyKey))
                .Select(p => p.Key)
                .ToImmutableArray();
        }
    }
}