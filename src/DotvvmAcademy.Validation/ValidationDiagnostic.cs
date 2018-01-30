namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnostic
    {
        public ValidationDiagnostic(string id, string message,
            ValidationDiagnosticSeverity severity)
        {
            Id = id;
            Message = message;
        }

        public string Id { get; }

        public string Message { get; }

        public ValidationDiagnosticSeverity Severity { get; }
    }
}