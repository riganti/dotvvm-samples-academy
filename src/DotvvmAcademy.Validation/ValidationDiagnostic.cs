namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnostic : IValidationDiagnostic
    {
        public ValidationDiagnostic(
            string message, 
            int start = -1, 
            int end = -1, 
            ValidationSeverity severity = ValidationSeverity.Error, 
            object underlyingObject = null)
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