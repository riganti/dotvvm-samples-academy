using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest
        : IValidationRequest<ICSharpValidationItem, ICSharpValidationRequirement>
    {
        public IList<ICSharpValidationItem> Items { get; } = new List<ICSharpValidationItem>();

        public IList<ICSharpValidationRequirement> Requirements = new List<ICSharpValidationRequirement>();
    }
}