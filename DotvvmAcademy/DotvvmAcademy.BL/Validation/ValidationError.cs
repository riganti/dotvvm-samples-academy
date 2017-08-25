using System;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation
{
    public class ValidationError : IEquatable<ValidationError>
    {
        public ValidationError(string message, bool isGlobal = true, int startPosition = default(int), int endPosition = default(int))
        {
            Message = message;
            IsGlobal = isGlobal;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public int EndPosition { get; }

        public bool IsGlobal { get; }

        public string Message { get; }

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
            return EndPosition == error.EndPosition &&
                IsGlobal == error.IsGlobal &&
                Message == error.Message &&
                StartPosition == error.StartPosition;
        }

        public override int GetHashCode()
        {
            var hashCode = -2080722901;
            hashCode = hashCode * -1521134295 + EndPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + IsGlobal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            hashCode = hashCode * -1521134295 + StartPosition.GetHashCode();
            return hashCode;
        }
    }
}