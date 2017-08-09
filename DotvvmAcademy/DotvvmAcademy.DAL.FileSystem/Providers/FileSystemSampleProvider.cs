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

        public string Get(Lesson lesson, int stepIndex, string path)
        {
            var stepPath = lesson.Steps[stepIndex];
            stepPath = stepPath.Substring(0, stepPath.LastIndexOfAny(new[] { '\\', '/' }));
            return GetFile(Path.Combine(lesson.DirectoryPath, stepPath, path));
        }

        public IQueryable<string> GetQueryable(Lesson lesson, int stepIndex, IEnumerable<string> paths)
        {
            paths = paths.Select(p=> Get(lesson, stepIndex, p));
            return GetFiles(paths).AsQueryable();
        }
    }
}