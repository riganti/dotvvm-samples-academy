using System;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    public class ValidatorDesignException : Exception
    {
        public ValidatorDesignException(string message, MethodInfo validator) : base(message)
        {
            Validator = validator;
        }

        public ValidatorDesignException(string message, MethodInfo validator, Exception innerException) : base(message, innerException)
        {
            Validator = validator;
        }

        public MethodInfo Validator { get; }
    }
}