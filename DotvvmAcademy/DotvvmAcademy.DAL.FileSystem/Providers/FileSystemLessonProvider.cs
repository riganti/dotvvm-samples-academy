using System;
using System.Linq;
using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemLessonProvider : ILessonProvider
    {
        public IQueryable<Lesson> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}