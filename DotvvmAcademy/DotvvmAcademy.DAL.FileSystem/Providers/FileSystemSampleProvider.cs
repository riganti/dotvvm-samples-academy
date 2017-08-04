using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemSampleProvider : FileSystemFileProvider, ISampleProvider
    {
        public FileSystemSampleProvider(string applicationRoot) : base(applicationRoot)
        {
        }

        public string Get(Lesson lesson, string path)
        {
            return GetFile(Path.Combine(lesson.DirectoryPath, path));
        }

        public IQueryable<string> GetQueryable(Lesson lesson, IEnumerable<string> paths)
        {
            paths = paths.Select(path => Path.Combine(lesson.DirectoryPath, path));
            return GetFiles(paths).AsQueryable();
        }
    }
}