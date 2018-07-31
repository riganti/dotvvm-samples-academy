using DotvvmAcademy.Validation;
using System;

namespace Microsoft.CodeAnalysis
{
    public static class DiagnosticSeverityExtensions
    {
        public static ValidationSeverity ToValidationSeverity(this DiagnosticSeverity severity)
        {
            switch (severity)
            {
                case DiagnosticSeverity.Error:
                    return ValidationSeverity.Error;

                case DiagnosticSeverity.Info:
                    return ValidationSeverity.Info;

                case DiagnosticSeverity.Warning:
                    return ValidationSeverity.Warning;

                case DiagnosticSeverity.Hidden:
                    return ValidationSeverity.Hint;

                default:
                    throw new NotSupportedException($"DiagnosticSeverity '{severity}' is not supported.");
            }
        }
    }
}