using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class FileSystemEnvironment : ICourseEnvironment
    {
        public FileSystemEnvironment(DirectoryInfo root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (!root.Exists)
            {
                throw new ArgumentException("Directory does not exist.", nameof(root));
            }

            Root = root;
        }

        public DirectoryInfo Root { get; }

        public Task<bool> Exists(string path)
        {
            path = Path.Combine(Root.FullName, $".{path}");
            return Task.FromResult(File.Exists(path) || Directory.Exists(path));
        }

        public Task<IEnumerable<string>> GetDirectories(string path)
        {
            var directory = GetDirectoryInfo(path);
            if (!directory.Exists)
            {
                throw new ArgumentException($"Directory at '{path}' does not exist.", nameof(path));
            }

            return Task.FromResult(directory.GetDirectories().Select(d => d.Name));
        }

        public DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(Path.Combine(Root.FullName, $".{path}"));
        }

        public FileInfo GetFileInfo(string path)
        {
            return new FileInfo(Path.Combine(Root.FullName, $".{path}"));
        }

        public Task<IEnumerable<string>> GetFiles(string path)
        {
            var directory = GetDirectoryInfo(path);
            if (!directory.Exists)
            {
                throw new ArgumentException($"Directory at '{path}' does not exist.", nameof(path));
            }

            return Task.FromResult(directory.GetFiles().Select(f => f.Name));
        }

        public Stream OpenRead(string path)
        {
            var file = GetFileInfo(path);
            if (!file.Exists)
            {
                throw new ArgumentException($"File at '{path}' does not exist.", nameof(path));
            }

            return new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        }
    }
}