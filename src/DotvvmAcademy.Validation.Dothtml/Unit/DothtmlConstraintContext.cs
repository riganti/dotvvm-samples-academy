using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlConstraintContext<TResult> : IConstraintContext<TResult>
        where TResult : ValidationTreeNode
    {
        private readonly ValidationReporter reporter;

        public DothtmlConstraintContext(
            IServiceProvider provider,
            string xpath,
            ImmutableArray<TResult> result)
        {
            Provider = provider;
            XPath = xpath;
            Result = result;
            reporter = Provider.GetRequiredService<ValidationReporter>();
        }

        public ImmutableArray<TResult> Result { get; }

        public string XPath { get; }

        public IServiceProvider Provider { get; }

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