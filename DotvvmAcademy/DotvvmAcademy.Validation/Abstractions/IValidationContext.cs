namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationContext<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResponse
    {
        TRequest Request { get; }

        TResponse Response { get; }
    }
}