namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidatorBuilder<TValidator> where TValidator : IValidator
    {
        TValidator Build();
    }
}