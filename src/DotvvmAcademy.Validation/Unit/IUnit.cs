using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public interface IUnit
    {
        IServiceProvider Provider { get; }

        IEnumerable<IQuery<TResult>> GetQueries<TResult>();

        void AddQuery<TResult>(IQuery<TResult> query);
    }
}