using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService<TRequest, TResponse, TItem, TRequirement>
        where TRequest : IValidationRequest<TItem, TRequirement>
        where TResponse : IValidationResponse
        where TItem : IValidationItem
        where TRequirement : IValidationRequirement
    {
        Task<TResponse> Validate(TRequest request);
    }
}