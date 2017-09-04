using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.FileSystem.Loaders
{
    public interface ILoader<TEntity>
        where TEntity : IEntity
    {
        TEntity Load(IIndexItem<TEntity> item);

        IEnumerable<TEntity> LoadAll();
    }
}