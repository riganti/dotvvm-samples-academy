using DotvvmAcademy.Validation.Abstractions;
using DotvvmAcademy.Validation.CSharp.DynamicAnalysis;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest : IValidationRequest
    {
        public CSharpCompilation Compilation { get; set; }

        public CSharpDynamicAnalysisContext DynamicAnalysis { get; set; }

        public CSharpStaticAnalysisContext StaticAnalysis { get; set; }

        public Dictionary<string, SyntaxTree> FileTable { get; set; } = new Dictionary<string, SyntaxTree>();

        public CSharpValidationExtent ValidationExtent { get; set; } = CSharpValidationExtent.All;
    }
}