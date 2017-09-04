using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IProject : IEntity
    {
        List<ILesson> ReferencingLessons { get; }

        string ZipFilePath { get; }
    }
}