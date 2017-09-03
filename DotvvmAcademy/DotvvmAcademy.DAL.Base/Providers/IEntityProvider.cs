using DotvvmAcademy.DAL.Base.Entities;
using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface IEntityProvider<TEntity>
        where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetQueryable();
    }
}