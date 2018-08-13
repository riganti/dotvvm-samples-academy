using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        private readonly ConcurrentDictionary<object, Constraint<TResult>> constraints
            = new ConcurrentDictionary<object, Constraint<TResult>>();

        public Query(IUnit unit, string source)
        {
            Unit = unit;
            Source = source;
        }

        public string Source { get; }

        public IUnit Unit { get; }

        public IEnumerable<Constraint<TResult>> GetConstraints()
        {
            return constraints.Values;
        }

        public void SetConstraint(object key, Constraint<TResult> constraint)
        {
            constraints.AddOrUpdate(key, constraint, (k, v) => constraint);
        }
    }
}