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
}