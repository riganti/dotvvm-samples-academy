namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public class DothtmlValidatorException : ValidatorException<DothtmlValidate>
    {
        public DothtmlValidatorException(string message, IValidationObject<DothtmlValidate> validationObject) : base(message, validationObject)
        {
        }
    }
}