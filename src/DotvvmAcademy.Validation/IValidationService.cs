using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    ///<summary>
    /// A <see cref="IValidationRequest"/> processor.
    ///</summary>
    /// <typeparam name="TRequest">Type of the processed <see cref="IValidationRequest"/></typeparam>
    /// <typeparam name="TResponse">Type of the <see cref="IValidationResponse"/> result</typeparam>
    public interface IValidationService<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResponse
    {
        /// <summary>
        /// Processes the given <see cref="IValidationRequest"/>.
        /// </summary>
        Task<TResponse> Validate(TRequest request);
    }
}