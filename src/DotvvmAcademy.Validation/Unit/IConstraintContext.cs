using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IConstraintContext
    {
        IServiceProvider Provider { get; }

        ImmutableArray<object> Result { get; }
    }

    public interface IConstraintContext<TResult> : IConstraintContext
    {
        new ImmutableArray<TResult> Result { get; }
    }
}