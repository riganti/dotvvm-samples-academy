using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpConstraintContext<TResult> : IConstraintContext<TResult>
        where TResult : ISymbol
    {
        private readonly ValidationReporter reporter;

        public CSharpConstraintContext(
            IServiceProvider provider,
            MetadataName name,
            ImmutableArray<TResult> result)
        {
            Provider = provider;
            Name = name;
            Result = result;

            reporter = provider.GetRequiredService<ValidationReporter>();
        }

        public MetadataName Name { get; }

        public IServiceProvider Provider { get; }

        public ImmutableArray<TResult> Result { get; }

        ImmutableArray<object> IConstraintContext.Result => Result.CastArray<object>();

        public void Report(string message, ValidationSeverity severity = default)
            => reporter.Report(message, severity: severity);

        public void Report(string message, ISymbol symbol, ValidationSeverity severity = default)
        {
            foreach (var location in symbol.Locations)
            {
                reporter.Report(message, location.SourceSpan.Start, location.SourceSpan.End, symbol, severity);
            }
        }

        public void ReportAll(string message, ValidationSeverity severity = default)
        {
            foreach (var symbol in Result)
            {
                Report(message, symbol, severity);
            }
        }
    }
}