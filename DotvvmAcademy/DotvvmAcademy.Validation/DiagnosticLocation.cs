namespace DotvvmAcademy.Validation
{
    public class DiagnosticLocation
    {
        public DiagnosticLocation(int startPosition, int endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public static DiagnosticLocation None = new DiagnosticLocation(-1, -1);

        public int EndPosition { get; }

        public int StartPosition { get; }
    }
}