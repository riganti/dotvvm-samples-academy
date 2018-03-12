namespace DotvvmAcademy.Validation
{
    public sealed class ValidationDiagnosticDescriptor
    {
        public ValidationDiagnosticDescriptor(
            string id,
            string name,
            string message,
            ValidationDiagnosticSeverity severity)
        {
            Id = id;
            Message = message;
            Name = name;
            Severity = severity;
        }

        public string Id { get; }

        public string Message { get; }

        public string Name { get; }

        public ValidationDiagnosticSeverity Severity { get; }
    }
}