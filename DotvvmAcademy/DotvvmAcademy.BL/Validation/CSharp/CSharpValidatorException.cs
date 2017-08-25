namespace DotvvmAcademy.BL.Validation.CSharp
{
    public class CSharpValidatorException : ValidatorException<CSharpValidate>
    {
        public CSharpValidatorException(string message, IValidationObject<CSharpValidate> validationObject) : base(message, validationObject)
        {
        }
    }
}