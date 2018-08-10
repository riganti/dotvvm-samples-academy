using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlSourceCode : ISourceCode
    {
        private readonly string source;

        public DothtmlSourceCode(string source)
        {
            this.source = source;
        }

        public string GetContent()
        {
            return source;
        }
    }
}