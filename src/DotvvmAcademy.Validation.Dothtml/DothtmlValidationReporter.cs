using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlValidationReporter : ValidationReporter
    {
        private readonly DothtmlSourceCodeProvider sourceCodeProvider;

        public DothtmlValidationReporter(DothtmlSourceCodeProvider sourceCodeProvider)
        {
            this.sourceCodeProvider = sourceCodeProvider;
        }

        public void Report(string message, ValidationTreeNode node, ValidationSeverity severity = default)
        {
            var sourceCode = sourceCodeProvider.GetSourceCode(node.TreeRoot);
            Report(new ResolverDothtmlDiagnostic(message, node, sourceCode, severity));
        }
    }
}