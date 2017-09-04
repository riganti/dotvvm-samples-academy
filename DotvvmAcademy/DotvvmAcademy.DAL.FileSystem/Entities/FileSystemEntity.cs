using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.FileSystem.Index.Items;

namespace DotvvmAcademy.DAL.FileSystem.Entities
{
    public class FileSystemEntity : IEntity
    {
        public int Id => Item.Id;

        internal IIndexItem Item { get; set; }
    }
}