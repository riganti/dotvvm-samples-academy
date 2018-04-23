using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class DothtmlValidationDiagnostic : ValidationDiagnostic
    {
        private readonly DothtmlNode node;

        public DothtmlValidationDiagnostic(DothtmlNode node, string message)
        {
            this.node = node;
            Message = message;
            Location = new DothtmlValidationDiagnosticLocation(node);
        }

        public override string Id { get; } = "DOTHTML";

        public override ValidationDiagnosticLocation Location { get; }

        public override string Message { get; }

        public override string Name { get; } = "Dothtml Node Error";

        public override ValidationDiagnosticSeverity Severity { get; } = ValidationDiagnosticSeverity.Error;
    }
}
