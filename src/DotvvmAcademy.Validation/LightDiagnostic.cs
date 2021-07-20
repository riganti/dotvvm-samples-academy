#nullable enable

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

        public string? Source { get; set; }

        public int Start { get; set; }

        public static bool operator ==(LightDiagnostic left, LightDiagnostic right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LightDiagnostic left, LightDiagnostic right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(End, Message, Severity, Source, Start);
        }

        public override string ToString()
        {
            return $"{Source}({Start},{End}): {Severity}: {Message}";
        }

        public override bool Equals(object? obj)
        {
            return obj is LightDiagnostic diagnostic &&
                   Equals(diagnostic);
        }

        public bool Equals(LightDiagnostic other)
        {
            return End == other.End &&
                   Message == other.Message &&
                   Severity == other.Severity &&
                   Source == other.Source &&
                   Start == other.Start;
        }
    }
}
