using System;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IQuery
    {
        void AddConstraint(Action<IConstraintContext> constraint);
    }

    public interface IQuery<TResult> : IQuery
    {
        void AddConstraint(Action<IConstraintContext<TResult>> constraint);
    }
}