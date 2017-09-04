using DotvvmAcademy.DAL.Base.Entities;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface IEntityProvider<out TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetQueryable();
    }
}