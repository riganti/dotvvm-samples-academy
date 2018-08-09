namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnostic : IValidationDiagnostic
    {
        public ValidationDiagnostic(
            string message,
            int start,
            int end,
            object underlyingObject,
            ValidationSeverity severity)
        {
            Message = message;
            Start = start;
            End = end;
            Severity = severity;
        }

        public int End { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public int Start { get; }

        public object UnderlyingObject { get; }
    }
}