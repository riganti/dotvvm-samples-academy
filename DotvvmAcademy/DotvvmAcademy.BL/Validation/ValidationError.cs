using System;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation
{
    public class ValidationError : IEquatable<ValidationError>
    {
        public ValidationError(string message, int startPosition, int endPosition, IValidationObject<Validate> originator)
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
    }
}