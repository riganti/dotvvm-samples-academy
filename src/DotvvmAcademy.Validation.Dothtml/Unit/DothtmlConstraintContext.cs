using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlConstraintContext<TResult> : IConstraintContext<TResult>
        where TResult : ValidationTreeNode
    {
        private readonly ValidationReporter reporter;

        public DothtmlConstraintContext(
            ValidationReporter reporter,
            string xpath,
            ImmutableArray<TResult> result)
        {
            this.reporter = reporter;
            XPath = xpath;
            Result = result;
        }

        public ImmutableArray<TResult> Result { get; }

        public string XPath { get; }

        ImmutableArray<object> IConstraintContext.Result => Result.CastArray<object>();

        public void Report(
            string message,
            ValidationTreeNode node = null,
            ValidationSeverity severity = default)
            => reporter.Report(message, node, severity);

        public void ReportAll(string message, ValidationSeverity severity = default)
        {
            foreach (var node in Result)
            {
                Report(message, node, severity);
            }
        }
    }
}