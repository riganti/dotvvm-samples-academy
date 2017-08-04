using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemStepProvider : FileSystemFileProvider, IStepProvider
    {
        public FileSystemStepProvider(string rootPath) : base(rootPath)
        {
        }

        public string Get(Lesson lesson, int index) => GetFile(Path.Combine(lesson.DirectoryPath, lesson.Steps[index]));

        public IQueryable<string> GetQueryable(Lesson lesson) => GetFiles(lesson.Steps).AsQueryable();
    }
}
