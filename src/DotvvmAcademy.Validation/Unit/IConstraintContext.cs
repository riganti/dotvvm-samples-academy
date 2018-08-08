using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IConstraintContext<TResult>
    {
        IQuery<TResult> Query { get; }

        IServiceProvider Provider { get; }

        ImmutableArray<TResult> Result { get; }
    }
}