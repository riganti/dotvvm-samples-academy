using DotvvmAcademy.Validation;
using System;

namespace Microsoft.CodeAnalysis
{
    public static class DiagnosticSeverityExtensions
    {
        public static ValidationSeverity ToValidationSeverity(this DiagnosticSeverity severity)
        {
            return severity switch
            {
                DiagnosticSeverity.Error => ValidationSeverity.Error,
                DiagnosticSeverity.Info => ValidationSeverity.Info,
                DiagnosticSeverity.Warning => ValidationSeverity.Warning,
                DiagnosticSeverity.Hidden => ValidationSeverity.Hint,
                _ => throw new NotSupportedException($"DiagnosticSeverity '{severity}' is not supported."),
            };
        }
    }
}
