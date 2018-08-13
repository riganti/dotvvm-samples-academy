using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationReporter
    {
        void Report(IValidationDiagnostic diagnostic);

        IEnumerable<IValidationDiagnostic> GetReportedDiagnostics();

        ValidationSeverity WorstSeverity { get; }
    }
}