namespace DotvvmAcademy.CourseFormat
{
    public interface ICodeTaskDiagnostic
    {
        int End { get; }

        string Message { get; }

        CodeTaskDiagnosticSeverity Severity { get; }

        int Start { get; }
    }
}