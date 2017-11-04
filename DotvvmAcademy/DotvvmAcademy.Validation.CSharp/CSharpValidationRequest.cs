using DotvvmAcademy.Validation.Abstractions;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest : IValidationRequest
    {
        public CSharpCompilation Compilation { get; set; }

        public CSharpDynamicAnalysisContext DynamicAnalysis { get; set; }

        public CSharpStaticAnalysisContext StaticAnalysis { get; set; }

        public CSharpValidationExtent ValidationExtent { get; set; } = CSharpValidationExtent.All;
    }
}