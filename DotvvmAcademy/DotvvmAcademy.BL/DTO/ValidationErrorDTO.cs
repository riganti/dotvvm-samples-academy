using System;
using DotvvmAcademy.Validation;

namespace DotvvmAcademy.BL.DTO
{
    public class ValidationErrorDTO
    {
        internal ValidationErrorDTO()
        {
        }

        public int EndPosition { get; internal set; }

        public bool IsGlobal { get; internal set; }

        public string Message { get; internal set; }

        public int StartPosition { get; internal set; }

        internal static ValidationErrorDTO Create(ValidationError e)
        {
            var dto = new ValidationErrorDTO
            {
                StartPosition = e.StartPosition,
                EndPosition = e.EndPosition,
                IsGlobal = e.IsGlobal,
                Message = e.Message
            };
            return dto;
        }
    }
}