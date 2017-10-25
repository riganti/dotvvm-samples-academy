namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidator<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResponse
    {
        TResponse Validate(TRequest request);
    }
}