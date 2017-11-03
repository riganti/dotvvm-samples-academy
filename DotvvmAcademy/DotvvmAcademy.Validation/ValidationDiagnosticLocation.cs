namespace DotvvmAcademy.Validation
{
    public class ValidationDiagnosticLocation
    {
        public ValidationDiagnosticLocation(int startPosition, int endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public static ValidationDiagnosticLocation None = new ValidationDiagnosticLocation(-1, -1);

        public int EndPosition { get; }

        public int StartPosition { get; }
    }
}