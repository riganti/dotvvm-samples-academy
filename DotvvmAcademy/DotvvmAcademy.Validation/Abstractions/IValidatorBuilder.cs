namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidatorBuilder<TValidator, TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResponse
        where TValidator : IValidator<TRequest, TResponse>
    {
        TValidator Build();
    }
}