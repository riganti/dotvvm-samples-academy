using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class DothtmlValidationDiagnosticLocation : ValidationDiagnosticLocation
    {
        private readonly DothtmlNode node;

        public DothtmlValidationDiagnosticLocation(DothtmlNode node) : base(node.StartPosition, node.EndPosition)
        {
            this.node = node;
        }

        public override object GetNativeObject()
        {
            return node;
        }
    }
}
