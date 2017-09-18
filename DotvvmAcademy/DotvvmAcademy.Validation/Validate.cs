using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public abstract class Validate
    {
        public Validate(MethodInfo validator, string code, IEnumerable<string> dependencies)
        {
            Validator = validator;
            Code = code;
            Dependencies = dependencies;
            Init();
        }

        public string Code { get; }

        public IEnumerable<string> Dependencies { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public string TestingValue => $"TestingValue{Id}";

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        internal MethodInfo Validator { get; }

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