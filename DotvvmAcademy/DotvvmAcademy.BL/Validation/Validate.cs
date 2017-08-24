using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public IEnumerable<string> Dependencies { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public string TestingValue => $"TestingValue{Id}";

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