using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp
{
    public abstract class ValidationDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        public ValidationDiagnosticAnalyzer(MetadataCollection metadata)
        {
            Metadata = metadata;
        }

        public MetadataCollection Metadata { get; }
    }
}