using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public class GlobalValidationDiagnostic : IValidationDiagnostic
    {
        public GlobalValidationDiagnostic(string message, ValidationSeverity severity)
        {
            Message = message;
            Severity = severity;
        }

        public int End { get; } = -1;

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public ISourceCode Source { get; } = null;

        public int Start { get; } = -1;

        public object UnderlyingObject { get; } = null;
    }
}