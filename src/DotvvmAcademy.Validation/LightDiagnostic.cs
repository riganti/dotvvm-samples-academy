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
            var arguments = validationDiagnostic.Arguments.ToArray();
            if (arguments.Length > 0)
            {
                Message = string.Format(validationDiagnostic.Message, arguments);
            }
            else
            {
                Message = validationDiagnostic.Message;
            }
        }

        public int End { get; set; }

        public string Message { get; set; }

        public ValidationSeverity Severity { get; set; }

        public string Source { get; set; }

        public int Start { get; set; }
    }
}