using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpSourceCode : ISourceCode
    {
        private readonly string source;

        public CSharpSourceCode(string source)
        {
            this.source = source;
        }

        public Guid Id { get; }

        public string GetContent()
        {
            return source;
        }
    }
}