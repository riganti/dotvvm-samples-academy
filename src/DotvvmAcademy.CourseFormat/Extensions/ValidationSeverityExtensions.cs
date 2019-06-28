using DotvvmAcademy.CourseFormat;
using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationSeverityExtensions
    {
        public static CodeTaskDiagnosticSeverity ToCodeTaskDiagnosticSeverity(this ValidationSeverity severity)
        {
            switch (severity)
            {
                case ValidationSeverity.Error:
                    return CodeTaskDiagnosticSeverity.Error;

                case ValidationSeverity.Warning:
                    return CodeTaskDiagnosticSeverity.Warning;

                case ValidationSeverity.Info:
                    return CodeTaskDiagnosticSeverity.Info;

                case ValidationSeverity.Hint:
                    return CodeTaskDiagnosticSeverity.Hint;

                default:
                    throw new NotSupportedException($"{nameof(ValidationSeverity)} '{severity}' is not supported.");
            }
        }
    }
}