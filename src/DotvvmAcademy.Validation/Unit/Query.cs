using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public class Query<TResult> : IQuery<TResult>
    {
        private readonly ConcurrentDictionary<object, Constraint<TResult>> constraints
            = new ConcurrentDictionary<object, Constraint<TResult>>();

        public Query(IServiceProvider provider, string source)
        {
            Provider = provider;
            Source = source;
        }

        public IServiceProvider Provider { get; }

        public string Source { get; }

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