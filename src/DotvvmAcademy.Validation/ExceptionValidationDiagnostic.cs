using System;

namespace DotvvmAcademy.Validation
{
    public class ExceptionValidationDiagnostic : IValidationDiagnostic
    {
        public ExceptionValidationDiagnostic(
            Exception exception,
            int start,
            int end,
            ValidationSeverity severity)
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