namespace DotvvmAcademy.CourseFormat
{
    public interface ICodeTaskDiagnostic
    {
        int End { get; }

        CodeTaskDiagnosticSeverity Severity { get; }

        int Start { get; }
    }
}