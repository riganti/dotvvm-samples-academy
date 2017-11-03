namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnostic
    {
        public ValidationDiagnostic(string id, string message, ValidationDiagnosticLocation location)
        {
            Id = id;
            Message = message;
            Location = location;
        }

        public string Id { get; set; }

        public ValidationDiagnosticLocation Location { get; set; }

        public string Message { get; set; }
    }
}