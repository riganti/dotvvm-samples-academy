using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.Index;
using DotvvmAcademy.DAL.FileSystem.Index.Items;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemEntityProvider<TEntity> : IEntityProvider<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IIndex<IndexItem<TEntity>, TEntity> index;

        public FileSystemEntityProvider(IIndex<IndexItem<TEntity>, TEntity> index)
        {
            this.index = index;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return index.Items.Select(item => index.Loader.Load(item)).AsQueryable();
        }
    }
}