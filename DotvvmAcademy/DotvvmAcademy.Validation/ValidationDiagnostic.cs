namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnostic
    {
        public ValidationDiagnostic(string id, string message, DiagnosticLocation location)
        {
            Id = id;
            Message = message;
            Location = location;
        }

        public string Id { get; set; }

        public DiagnosticLocation Location { get; set; }

        public string Message { get; set; }
    }
}