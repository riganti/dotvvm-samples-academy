namespace DotvvmAcademy.Controls
{
    public class MonacoMarker
    {
        public string Code { get; set; }

        public int EndColumn { get; set; }

        public int EndLineNumber { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public MonacoSeverity Severity { get; set; }

        public int StartColumn { get; set; }

        public int StartLineNumber { get; set; }
    }
}