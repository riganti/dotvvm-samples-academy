using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Entities;

namespace DotvvmAcademy.DAL.FileSystem.Index.Items
{
    public interface IIndexItem<TEntity>
        where TEntity : class, IEntity
    {
        int Id { get; }

        string Path { get; }
    }
}