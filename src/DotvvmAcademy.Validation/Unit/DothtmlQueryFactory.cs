using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Unit
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IServiceProvider provider;

        public QueryFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IQuery<TResult> Create<TResult>(string queryString)
        {
            return ActivatorUtilities.CreateInstance<Query<TResult>>(provider, queryString);
        }
    }
}