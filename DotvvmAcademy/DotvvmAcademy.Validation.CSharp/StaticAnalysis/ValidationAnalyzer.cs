using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public abstract class ValidationAnalyzer : DiagnosticAnalyzer
    {
        public CSharpStaticAnalysisContext StaticAnalysis { get; set; }
    }
}