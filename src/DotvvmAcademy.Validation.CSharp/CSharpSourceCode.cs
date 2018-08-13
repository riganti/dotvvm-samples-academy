using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpSourceCode : ISourceCode
    {
        private readonly string source;

        public CSharpSourceCode(string source, string fileName, bool isValidated)
        {
            this.source = source;
            FileName = fileName;
            IsValidated = isValidated;
        }

        public string FileName { get; }

        public bool IsValidated { get; }

        public string GetContent()
        {
            return source;
        }
    }
}