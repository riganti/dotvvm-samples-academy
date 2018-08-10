using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter : IValidationReporter
    {
        private readonly List<IValidationDiagnostic> diagnostics = new List<IValidationDiagnostic>();

        public ValidationSeverity WorstSeverity { get; private set; } = (ValidationSeverity)int.MaxValue;

        public IEnumerable<IValidationDiagnostic> GetReportedDiagnostics()
        {
            return diagnostics;
        }

        public void Report(IValidationDiagnostic diagnostic)
        {
            if (diagnostic.Severity < WorstSeverity)
            {
                WorstSeverity = diagnostic.Severity;
            }

            diagnostics.Add(diagnostic);
        }
    }
}