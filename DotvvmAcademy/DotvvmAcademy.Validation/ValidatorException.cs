using System;
using System.Reflection;

namespace DotvvmAcademy.Validation
{
    /// <summary>
    /// An exception that happend inside a validator.
    /// </summary>
    public class ValidatorException : Exception
    {
        public ValidatorException(string message, MethodInfo validator, Exception innerException) : base(message, innerException)
        {
            Validator = validator;
        }

        public MethodInfo Validator { get; }
    }

    public abstract class ValidatorException<TValidate> : Exception
        where TValidate : Validate
    {
        public ValidatorException(string message, IValidationObject<TValidate> validationObject) : base(message)
        {
            ValidationObject = validationObject;
        }


        public IValidationObject<TValidate> ValidationObject { get; }
    }
}