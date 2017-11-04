using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public abstract class ValidationAnalyzer : DiagnosticAnalyzer
    {
        public CSharpStaticAnalysisContext ValidationAnalyzerContext { get; set; }
    }
}