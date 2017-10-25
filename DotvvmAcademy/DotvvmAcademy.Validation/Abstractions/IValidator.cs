using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidator<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResponse
    {
        Task<TResponse> Validate(TRequest request);
    }
}