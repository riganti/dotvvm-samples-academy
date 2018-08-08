using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Unit
{
    public abstract class Unit : IUnit
    {
        private readonly ConcurrentDictionary<object, object> queries = new ConcurrentDictionary<object, object>();
        private readonly IQueryFactory queryFactory;

        public Unit(IServiceProvider provider)
        {
            Provider = provider;
            queryFactory = provider.GetRequiredService<IQueryFactory>();
        }

        public IServiceProvider Provider { get; }

        public IEnumerable<IQuery<TResult>> GetQueries<TResult>()
        {
            return queries.Values.OfType<IQuery<TResult>>();
        }

        public IQuery<TResult> GetQuery<TResult>(string queryString)
        {
            var query = queryFactory.Create<TResult>(queryString);
            queries.AddOrUpdate(query.Source, query, (k, v) => query);
            return query;
        }
    }
}