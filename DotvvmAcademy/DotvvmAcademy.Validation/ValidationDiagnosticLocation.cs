namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnosticLocation
    {
        public ValidationDiagnosticLocation(int start, int end, string fileKey)
        {
            Start = start;
            End = end;
            FileKey = fileKey;
        }

        public static ValidationDiagnosticLocation None = new ValidationDiagnosticLocation(-1, -1, null);

        public int End { get; }

        public int Start { get; }

        public string FileKey { get; }
    }
}