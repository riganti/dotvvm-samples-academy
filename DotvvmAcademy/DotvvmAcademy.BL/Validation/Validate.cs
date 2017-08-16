using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Validation
{
    public abstract class Validate
    {
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public string Code { get; }

        public Validate(string code)
        {
            Code = code;
            Init();
        }

        protected abstract void Init();

        public void AddError(string message, int startPosition, int endPosition)
        {
            ValidationErrors.Add(new ValidationError(message, startPosition, endPosition));
        }
    }
}
