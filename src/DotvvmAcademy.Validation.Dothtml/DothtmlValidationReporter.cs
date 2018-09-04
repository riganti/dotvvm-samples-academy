using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

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
            if (node is ValidationTreeRoot)
            {
                this.Report(message, severity);
                return;
            }

            var sourceCode = sourceCodeProvider.GetSourceCode(node.TreeRoot);
            Report(new ResolverDothtmlDiagnostic(message, node, sourceCode, severity));
        }

        public void Report(string message, DothtmlNode node, DothtmlSourceCode source, ValidationSeverity severity = default)
        {
            if (node is DothtmlRootNode)
            {
                this.Report(message, severity);
                return;
            }

            Report(new ParserDothtmlDiagnostic(message, node, source, severity));
        }
    }
}