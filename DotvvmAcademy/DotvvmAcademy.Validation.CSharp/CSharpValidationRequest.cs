using DotvvmAcademy.Validation.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest : IValidationRequest
    {
        public bool AllowAssemblyRewrite { get; set; } = true;

        public bool AllowCompilerDiagnostics { get; set; } = true;

        public bool AllowDynamicAnalysis { get; set; } = true;

        public bool AllowStaticAnalysis { get; set; } = true;

        public CSharpCompilation Compilation { get; set; }

        public List<CSharpValidationUnit> ValidationUnits { get; } = new List<CSharpValidationUnit>();
    }
}