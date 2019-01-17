using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter : IValidationReporter
    {
        private readonly List<IValidationDiagnostic> diagnostics = new List<IValidationDiagnostic>();
        private ValidationSeverity worstSeverity = (ValidationSeverity)int.MaxValue;

        public ValidationReporter(SourceCodeStorage sourceCodeStorage)
        {
            SourceCodeStorage = sourceCodeStorage;
        }

        public SourceCodeStorage SourceCodeStorage { get; }

        public IEnumerable<IValidationDiagnostic> GetDiagnostics()
        {
            return diagnostics;
        }

        public ValidationSeverity GetWorstSeverity()
        {
            return worstSeverity;
        }

        public void Report(IValidationDiagnostic diagnostic)
        {
            if (diagnostic.Severity < worstSeverity)
            {
                worstSeverity = diagnostic.Severity;
            }
            diagnostics.Add(diagnostic);
        }
    }
}