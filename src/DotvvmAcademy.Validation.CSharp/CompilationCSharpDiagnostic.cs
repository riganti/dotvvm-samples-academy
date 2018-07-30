using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CompilationCSharpDiagnostic : IValidationDiagnostic
    {
        public CompilationCSharpDiagnostic(Diagnostic diagnostic)
        {
            Diagnostic = diagnostic;
            Message = diagnostic.GetMessage();
            Severity = diagnostic.Severity.ToValidationSeverity();
        }

        public Diagnostic Diagnostic { get; }

        public int End => Diagnostic.Location.SourceSpan.Start;

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public int Start => Diagnostic.Location.SourceSpan.End;

        object IValidationDiagnostic.UnderlyingObject => Diagnostic;
    }
}