using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public class ConstraintContext
    {
        public ConstraintContext(IServiceProvider provider)
        {
            Provider = provider;
        }

        IServiceProvider Provider { get; }
    }

    public class ConstraintContext<TResult>
    {
        public ConstraintContext(
            IServiceProvider provider,
            IQuery<TResult> query,
            ImmutableArray<TResult> result)
        {
            Provider = provider;
            Query = query;
            Result = result;
        }

        public IServiceProvider Provider { get; }

        public IQuery<TResult> Query { get; }

        public ImmutableArray<TResult> Result { get; }
    }
}