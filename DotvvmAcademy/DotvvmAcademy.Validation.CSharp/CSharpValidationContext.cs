using DotvvmAcademy.Validation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationContext : IValidationContext<CSharpValidationRequest, CSharpValidationResponse>
    {
        public CSharpValidationContext(CSharpValidationRequest request)
        {
            Request = request;
        }

        public CSharpValidationRequest Request { get; }

        public CSharpValidationResponse Response { get; } = new CSharpValidationResponse();
    }
}