namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskDiagnostic
    {
        public CodeTaskDiagnostic(string message, int start, int end, CodeTaskDiagnosticSeverity severity)
        {
            Message = message;
            Start = start;
            End = end;
            Severity = severity;
        }

        public int End { get; }

        public string Message { get; }

        public CodeTaskDiagnosticSeverity Severity { get; }

        public int Start { get; }
    }
}