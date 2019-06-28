namespace DotvvmAcademy.Web.Pages.Step
{
    public class MonacoMarker
    {
        public MonacoMarker(
            string message,
            MonacoSeverity severity,
            int startLineNumber,
            int startColumn,
            int endLineNumber,
            int endColumn)
        {
            Message = message;
            Severity = severity;
            StartLineNumber = startLineNumber;
            StartColumn = startColumn;
            EndLineNumber = endLineNumber;
            EndColumn = endColumn;
        }

        public int EndColumn { get; }

        public int EndLineNumber { get; }

        public string Message { get; }

        public MonacoSeverity Severity { get; }

        public int StartColumn { get; }

        public int StartLineNumber { get; }
    }
}