using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpSourceCode : ISourceCode
    {
        private readonly string source;

        public CSharpSourceCode(string source)
        {
            this.source = source;
        }

        public bool IsValidated { get; } = true;

        public override string ToString()
        {
            return source;
        }
    }
}