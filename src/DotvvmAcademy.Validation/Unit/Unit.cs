using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class Unit : IUnit
    {
        private readonly HashSet<object> queries = new HashSet<object>();

        public Unit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IServiceProvider Provider { get; }

        public void AddQuery<TResult>(IQuery<TResult> query)
        {
            queries.Add(query);
        }

        public IEnumerable<IQuery<TResult>> GetQueries<TResult>()
        {
            return queries.OfType<IQuery<TResult>>();
        }
    }
}