using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlSourceCode : ISourceCode
    {
        private readonly DothtmlRootNode rootNode;
        private readonly string source;

        public DothtmlSourceCode(string source, bool isValidated = false)
        {
            this.source = source;
        }
    }

        public bool IsValidated { get; }
    }
}
