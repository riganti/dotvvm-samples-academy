using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using DotvvmAcademy.DAL.FileSystem.Loaders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Index
{
    public interface IIndex<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<IIndexItem<TEntity>> Items { get; }

        ILoader<TEntity> Loader { get; }

        Task Load();
    }
}