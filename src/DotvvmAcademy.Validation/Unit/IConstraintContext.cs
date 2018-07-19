using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IConstraintContext
    {
        ImmutableArray<object> Result { get; }
    }

    public interface IConstraintContext<TResult> : IConstraintContext
    {
        new ImmutableArray<TResult> Result { get; }
    }
}