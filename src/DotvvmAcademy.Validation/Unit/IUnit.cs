using System;
using System.Collections;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IUnit
    {
        IServiceProvider Provider { get; }

        IQuery<TResult> GetQuery<TResult>(string queryString);

        IEnumerable<IQuery<TResult>> GetQueries<TResult>();
    }
}