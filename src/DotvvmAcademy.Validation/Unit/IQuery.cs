using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IQuery<TResult>
    {
        IServiceProvider Provider { get; }

        string Source { get; }

        IEnumerable<Constraint<TResult>> GetConstraints();

        void SetConstraint(object key, Constraint<TResult> constraint);
    }
}