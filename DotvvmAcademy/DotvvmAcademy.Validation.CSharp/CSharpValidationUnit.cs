using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationUnit
    {
        public CSharpSyntaxTree SyntaxTree { get; set; }

        public List<string> ValidationMethods { get; set; } = new List<string>();
    }
}