using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter
    {
        private List<IValidationDiagnostic> diagnostics = new List<IValidationDiagnostic>();

        public void Report(IValidationDiagnostic diagnostic) => diagnostics.Add(diagnostic);

        public void Report(
            string message,
            int start = -1,
            int end = -1,
            ValidationSeverity severity = ValidationSeverity.Error,
            object underlyingObject = null)
            => Report(new ValidationDiagnostic(message, start, end, severity, underlyingObject));
    }
}