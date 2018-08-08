using System;

namespace DotvvmAcademy.Validation
{
    public class ExceptionValidationDiagnostic : IValidationDiagnostic
    {
        public ExceptionValidationDiagnostic(
            Exception exception,
            int start = -1,
            int end = -1,
            ValidationSeverity severity = default)
        {
            Exception = exception;
            Start = start;
            End = end;
            Severity = severity;
        }

        public int End { get; }

        public Exception Exception { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public int Start { get; }

        object IValidationDiagnostic.UnderlyingObject => Exception;
    }
}