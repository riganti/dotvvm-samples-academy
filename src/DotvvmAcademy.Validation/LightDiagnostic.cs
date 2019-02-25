using System;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    [Serializable]
    public struct LightDiagnostic
    {
        public LightDiagnostic(IValidationDiagnostic validationDiagnostic)
        {
            Start = validationDiagnostic.Start;
            End = validationDiagnostic.End;
            Severity = validationDiagnostic.Severity;
            Source = validationDiagnostic.Source?.FileName;
            Message = string.Format(validationDiagnostic.Message, validationDiagnostic.Arguments.ToArray());
        }

        public int End { get; set; }

        public string Message { get; set; }

        public ValidationSeverity Severity { get; set; }

        public string Source { get; set; }

        public int Start { get; set; }
    }
}