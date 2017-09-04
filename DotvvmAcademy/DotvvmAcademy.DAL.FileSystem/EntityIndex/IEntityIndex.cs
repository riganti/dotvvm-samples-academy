using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.EntityIndex
{
    public interface IEntityIndex<TIndexEntity>
        where TIndexEntity : IndexEntity
    {
        List<TIndexEntity> Items { get; set; }

        Task Create();
    }
}