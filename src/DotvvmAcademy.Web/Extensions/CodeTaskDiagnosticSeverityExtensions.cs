using DotvvmAcademy.Web.Pages.Step;
using System;

namespace DotvvmAcademy.CourseFormat
{
    public static class CodeTaskDiagnosticSeverityExtensions
    {
        public static MonacoSeverity ToMonacoSeverity(this CodeTaskDiagnosticSeverity severity)
        {
            switch (severity)
            {
                case CodeTaskDiagnosticSeverity.Error:
                    return MonacoSeverity.Error;

                case CodeTaskDiagnosticSeverity.Warning:
                    return MonacoSeverity.Warning;

                case CodeTaskDiagnosticSeverity.Info:
                    return MonacoSeverity.Info;

                case CodeTaskDiagnosticSeverity.Hint:
                    return MonacoSeverity.Hint;

                default:
                    throw new NotSupportedException($"{nameof(CodeTaskDiagnosticSeverity)} '{severity}' is not supported.");
            }
        }
    }
}