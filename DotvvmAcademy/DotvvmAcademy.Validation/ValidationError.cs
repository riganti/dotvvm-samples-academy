using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation
{
    public class ValidationError : IEquatable<ValidationError>
    {
        public ValidationError(string message, int startPosition, int endPosition, IValidationObject<Validate> originator = null)
        {
            Message = message;
            IsGlobal = false;
            StartPosition = startPosition;
            EndPosition = endPosition;
            Originator = originator;
        }

        public ValidationError(string message, IValidationObject<Validate> originator = null)
        {
            Message = message;
            IsGlobal = true;
            Originator = originator;
        }

        public int EndPosition { get; }

        public bool IsGlobal { get; }

        public string Message { get; }

        public IValidationObject<Validate> Originator { get; }

        public int StartPosition { get; }

        public static ValidationError FromString(string line)
        {
            var args = CommandLineUtils.ParseArguments(line);
            var exception = new ArgumentException("The passed string doesn't contain a ValidationError.", nameof(line));
            if (args.Length <= 1 || !(args.Length == 2 || args.Length == 4))
            {
                throw exception;
            }

            if (args[0] != nameof(ValidationError))
            {
                throw exception;
            }

            if (args.Length == 2)
            {
                return new ValidationError(args[1]);
            }
            else
            {
                return new ValidationError(args[1], Convert.ToInt32(args[2]), Convert.ToInt32(args[3]));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ValidationError e)
            {
                return Equals(e);
            }

            return false;
        }

        public bool Equals(ValidationError error)
        {
            return EndPosition.Equals(error.EndPosition) &&
                IsGlobal.Equals(error.IsGlobal) &&
                Message.Equals(error.Message) &&
                StartPosition.Equals(error.StartPosition);
        }

        public override int GetHashCode()
        {
            var hashCode = -165341017;
            hashCode = hashCode * -1521134295 + EndPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + IsGlobal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            hashCode = hashCode * -1521134295 + StartPosition.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"\"{typeof(ValidationError).AssemblyQualifiedName}\" ");
            sb.Append($"\"{Message}\"");
            if (!IsGlobal)
            {
                sb.Append(' ');
                sb.Append(StartPosition.ToString());
                sb.Append(' ');
                sb.Append(EndPosition.ToString());
            }
            return sb.ToString();
        }
    }
}