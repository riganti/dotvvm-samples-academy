using System;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation
{
    public abstract class Validate
    {
        public Validate(string code, IEnumerable<string> dependencies)
        {
            Code = code;
            Dependencies = dependencies;
            Init();
        }

        public string Code { get; }

        public IEnumerable<string> Dependencies { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public string TestingValue => $"TestingValue{Id}";

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public void AddError(string message, int startPosition, int endPosition, IValidationObject<Validate> originator)
        {
            var error = new ValidationError(message, startPosition, endPosition, originator);
            if (!ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }

        public void AddGlobalError(string message, IValidationObject<Validate> originator = null)
        {
            var error = new ValidationError(message, originator);
            if (!ValidationErrors.Contains(error))
            {
                ValidationErrors.Add(error);
            }
        }

        public void RemoveErrorsFrom(IValidationObject<Validate> obj)
        {
            ValidationErrors.RemoveAll(e => e.Originator.Equals(obj));
        }

        protected abstract void Init();
    }
}