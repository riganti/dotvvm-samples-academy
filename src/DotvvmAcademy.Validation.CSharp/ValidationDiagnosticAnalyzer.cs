using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp
{
    public abstract class ValidationDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        public ValidationDiagnosticAnalyzer(OldMetadataCollection metadata)
        {
            Metadata = metadata;
        }

        public OldMetadataCollection Metadata { get; }
    }
}