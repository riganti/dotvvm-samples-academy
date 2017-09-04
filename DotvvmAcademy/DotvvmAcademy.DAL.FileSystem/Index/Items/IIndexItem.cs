using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Entities;

namespace DotvvmAcademy.DAL.FileSystem.Index.Items
{
    public interface IIndexItem
    {
        int Id { get; }

        string Path { get; }
    }

    public interface IIndexItem<TEntity> : IIndexItem
        where TEntity : class, IEntity
    {
        int Id { get; }

        string Path { get; }
    }
}