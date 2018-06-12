namespace DotvvmAcademy.CourseFormat
{
    internal class CodeTaskDiagnostic : ICodeTaskDiagnostic
    {
        public int End { get; set; }

        public string Message { get; set; }

        public CodeTaskDiagnosticSeverity Severity { get; set; }

        public int Start { get; set; }
    }
}