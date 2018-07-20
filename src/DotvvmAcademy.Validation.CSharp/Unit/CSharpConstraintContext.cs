using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpConstraintContext<TResult> : IConstraintContext<TResult>
        where TResult : ISymbol
    {
        private readonly ValidationReporter reporter;

        public CSharpConstraintContext(
            ValidationReporter reporter,
            MetadataName name,
            ImmutableArray<TResult> result)
        {
            this.reporter = reporter;
            Name = name;
            Result = result;
        }

        public MetadataName Name { get; }

        public ImmutableArray<TResult> Result { get; }

        ImmutableArray<object> IConstraintContext.Result => Result.CastArray<object>();
    }
}