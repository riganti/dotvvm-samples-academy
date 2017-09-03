using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Repositories
{
    interface IRepository<TEntity, TKey>
    {
        Task<TEntity> GetById(TKey id);
    }
}
