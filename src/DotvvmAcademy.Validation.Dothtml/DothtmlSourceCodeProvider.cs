using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlSourceCodeProvider
    {
        public DothtmlSourceCode GetSourceCode(ValidationTreeRoot root)
        {
            // TODO: This does not seem particularly clean
            return root?.SourceCode;
        }

        public void Register(DothtmlSourceCode sourceCode, DothtmlRootNode parserNode, ValidationTreeNode resolverNode)
        {
        }
    }
}