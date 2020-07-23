using DotvvmAcademy.CourseFormat;
using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationSeverityExtensions
    {
        public static CodeTaskDiagnosticSeverity ToCodeTaskDiagnosticSeverity(this ValidationSeverity severity)
        {
            return severity switch
            {
                ValidationSeverity.Error => CodeTaskDiagnosticSeverity.Error,
                ValidationSeverity.Warning => CodeTaskDiagnosticSeverity.Warning,
                ValidationSeverity.Info => CodeTaskDiagnosticSeverity.Info,
                ValidationSeverity.Hint => CodeTaskDiagnosticSeverity.Hint,
                _ => throw new NotSupportedException($"{nameof(ValidationSeverity)} '{severity}' is not supported."),
            };
        }
    }
}
