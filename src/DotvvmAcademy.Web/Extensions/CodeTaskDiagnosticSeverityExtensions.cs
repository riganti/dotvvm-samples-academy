using DotvvmAcademy.Web.Pages.Step;
using System;

namespace DotvvmAcademy.CourseFormat
{
    public static class CodeTaskDiagnosticSeverityExtensions
    {
        public static MonacoSeverity ToMonacoSeverity(this CodeTaskDiagnosticSeverity severity)
        {
            return severity switch
            {
                CodeTaskDiagnosticSeverity.Error => MonacoSeverity.Error,
                CodeTaskDiagnosticSeverity.Warning => MonacoSeverity.Warning,
                CodeTaskDiagnosticSeverity.Info => MonacoSeverity.Info,
                CodeTaskDiagnosticSeverity.Hint => MonacoSeverity.Hint,
                _ => throw new NotSupportedException($"{nameof(CodeTaskDiagnosticSeverity)} '{severity}' is not supported."),
            };
        }
    }
}
