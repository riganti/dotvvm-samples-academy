using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Loaders
{
    public interface ILoader<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> Load(IIndexItem<TEntity> item);

        Task<IEnumerable<TEntity>> LoadAll();
    }
}