using System.Collections.Generic;
using System.IO;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemFileProvider
    {
        public FileSystemFileProvider(string rootPath)
        {
            RootPath = rootPath;
        }

        public string RootPath { get; set; }

        protected string GetFile(string relativePath)
        {
            var absolutePath = Path.Combine(RootPath, relativePath);
            return File.ReadAllText(absolutePath);
        }

        protected IEnumerable<string> GetFiles(IEnumerable<string> relativePaths)
        {
            foreach (var path in relativePaths)
            {
                yield return GetFile(path);
            }
        }
    }
}