using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public class LightDiagnostic : IEquatable<LightDiagnostic>
    {
        public LightDiagnostic()
        {
            End = -1;
            Start = -1;
            Message = "A Diagnostic creation error has occurred.";
            Severity = ValidationSeverity.Error;
            Source = string.Empty;
        }

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

        public static bool operator ==(LightDiagnostic left, LightDiagnostic right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LightDiagnostic left, LightDiagnostic right)
        {
            return !left.Equals(right);
        }

        public bool Equals(LightDiagnostic other)
        {
            return Start.Equals(other.Start)
                && End.Equals(other.End)
                && Message.Equals(other.Message)
                && Severity.Equals(other.Severity)
                && Source.Equals(other.Source);
        }

        public override bool Equals(object obj)
        {
            if (obj is LightDiagnostic light)
            {
                return Equals(light);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(End, Message, Severity, Source, Start);
        }

        public override string ToString()
        {
            return $"{Source}({Start},{End}): {Severity}: {Message}";
        }
    }
}
