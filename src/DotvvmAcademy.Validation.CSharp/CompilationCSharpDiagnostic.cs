using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CompilationCSharpDiagnostic : IValidationDiagnostic
    {
        public CompilationCSharpDiagnostic(Diagnostic diagnostic, CSharpSourceCode source)
        {
            Diagnostic = diagnostic;
            Source = source;
            Message = diagnostic.GetMessage();
            Severity = diagnostic.Severity.ToValidationSeverity();
        }

        public IEnumerable<object> Arguments { get; } = Enumerable.Empty<object>();

        public Diagnostic Diagnostic { get; }

        public int End => Diagnostic.Location.SourceSpan.End;

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public CSharpSourceCode Source { get; }

        public int Start => Diagnostic.Location.SourceSpan.Start;

        ISourceCode IValidationDiagnostic.Source => Source;

        object IValidationDiagnostic.UnderlyingObject => Diagnostic;
    }
}