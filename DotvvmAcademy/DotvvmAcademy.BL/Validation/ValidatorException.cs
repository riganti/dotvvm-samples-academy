using System;

namespace DotvvmAcademy.BL.Validation
{
    public abstract class ValidatorException<TValidate> : Exception
        where TValidate : Validate
    {
        public ValidatorException(string message, IValidationObject<TValidate> validationObject) : base(message)
        {
            ValidationObject = validationObject;
        }

        public IValidationObject<TValidate> ValidationObject { get; set; }
    }
}