using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public abstract class DothtmlConstraintContext
    {
        private readonly ValidationReporter reporter;

        public DothtmlConstraintContext(ValidationReporter reporter, string xpath, ImmutableArray<ValidationTreeNode> result)
        {
            this.reporter = reporter;
            XPath = xpath;
            Result = result;
        }

        public ImmutableArray<ValidationTreeNode> Result { get; }

        public string XPath { get; }

        public void Report(
            string message,
            ValidationSeverity severity = ValidationSeverity.Error,
            ValidationTreeNode node = null)
            => reporter.Report(message, severity, node);

        public void ReportAll(string message, ValidationSeverity severity = ValidationSeverity.Error)
        {
            foreach (var node in Result)
            {
                Report(message, severity, node);
            }
        }
    }

    public abstract class DothtmlConstraintContext<TResult> : DothtmlConstraintContext
        where TResult : ValidationTreeNode
    {
        public DothtmlConstraintContext(ValidationReporter reporter, string xpath, ImmutableArray<TResult> result)
            : base(reporter, xpath, result.CastArray<ValidationTreeNode>())
        {
            Result = result;
        }

        public new ImmutableArray<TResult> Result { get; }
    }
}