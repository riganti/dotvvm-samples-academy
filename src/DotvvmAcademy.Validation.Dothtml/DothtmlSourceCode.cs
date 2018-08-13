using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlSourceCode : ISourceCode
    {
        private readonly string source;

        public DothtmlSourceCode(string source, string fileName, bool isValidated)
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