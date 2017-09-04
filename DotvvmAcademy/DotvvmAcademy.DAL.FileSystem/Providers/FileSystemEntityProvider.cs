using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.Index;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemEntityProvider<TEntity> : IEntityProvider<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IIndex<TEntity> index;

        public FileSystemEntityProvider(IIndex<TEntity> index)
        {
            this.index = index;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return index.Items.Select(async item => await index.Loader.Load(item)).AsQueryable();
        }
    }
}