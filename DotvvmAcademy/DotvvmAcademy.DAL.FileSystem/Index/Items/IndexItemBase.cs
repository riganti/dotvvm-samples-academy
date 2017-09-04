using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.FileSystem.Index.Items
{
    public abstract class IndexItemBase<TEntity> : IIndexItem<TEntity>
        where TEntity : IEntity
    {
        public int Id { get; set; }

        public string Path { get; set; }
    }
}