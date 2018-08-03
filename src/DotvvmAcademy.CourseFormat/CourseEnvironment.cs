using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseEnvironment
    {
        public const string ResourcesDirectory = "resources";
        public const string ContentDirectory = "content";

        public CourseEnvironment(DirectoryInfo root)
        {
            Root = root;
        }

        public DirectoryInfo Root { get; }

        public bool Exists(string sourcePath)
        {
            var path = Path.Combine(Root.FullName, $".{sourcePath}");
            return File.Exists(path) || Directory.Exists(path);
        }

        public DirectoryInfo GetDirectory(string sourcePath)
        {
            return new DirectoryInfo(Path.Combine(Root.FullName, $".{sourcePath}"));
        }

        public FileInfo GetFile(string sourcePath)
        {
            return new FileInfo(Path.Combine(Root.FullName, $".{sourcePath}"));
        }

        public FileSystemInfo GetFileSystemInfo(string sourcePath)
        {
            var directory = GetDirectory(sourcePath);
            if (directory.Exists)
            {
                return directory;
            }

            return GetFile(sourcePath);
        }
    }
}