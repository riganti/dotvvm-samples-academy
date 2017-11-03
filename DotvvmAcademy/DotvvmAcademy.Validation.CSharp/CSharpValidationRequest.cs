using DotvvmAcademy.Validation.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest : IValidationRequest
    {
        public CSharpValidationExtent ValidationExtent { get; set; } = CSharpValidationExtent.All;

        public CSharpCompilation Compilation { get; set; }

        public List<CSharpValidationUnit> ValidationUnits { get; } = new List<CSharpValidationUnit>();
    }
}