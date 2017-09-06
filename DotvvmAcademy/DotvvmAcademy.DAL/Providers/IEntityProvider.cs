using DotvvmAcademy.DAL.Entities;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.Providers
{
    public interface IEntityProvider<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity> Get(string path);
    }
}