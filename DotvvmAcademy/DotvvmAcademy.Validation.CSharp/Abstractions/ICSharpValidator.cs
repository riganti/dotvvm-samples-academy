using DotvvmAcademy.Validation.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpValidator : IValidator<CSharpValidationRequest, CSharpValidationResponse>
    {
        ImmutableDictionary<string, CSharpStaticAnalysisContext> StaticAnalysisContexts { set; }
    }
}