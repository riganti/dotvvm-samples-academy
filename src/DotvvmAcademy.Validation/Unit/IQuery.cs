using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IQuery<TResult>
    {
        IUnit Unit { get; }

        string Source { get; }

        IEnumerable<Constraint<TResult>> GetConstraints();

        void SetConstraint(object key, Constraint<TResult> constraint);
    }
}