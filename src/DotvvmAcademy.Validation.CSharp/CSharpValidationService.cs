using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationService
        : IValidationService<CSharpValidationRequest, CSharpValidationResponse,
            ICSharpValidationItem, ICSharpValidationRequirement>
    {
        public Task<CSharpValidationResponse> Validate(CSharpValidationRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}