using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation
{
    public abstract class Validate
    {
        public Validate(string code)
        {
            Code = code;
            Init();
        }

        public string Code { get; }

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public void AddError(string message, int startPosition, int endPosition)
        {
            var error = new ValidationError(message, false, startPosition, endPosition);
            if (!ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }

        public void AddGlobalError(string message)
        {
            var error = new ValidationError(message);
            if (!ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }

        protected abstract void Init();
    }
}