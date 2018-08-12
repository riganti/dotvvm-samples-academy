using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.CourseFormat
{
    public class SourcePathStorage
    {
        private readonly ConcurrentDictionary<string, string> paths = new ConcurrentDictionary<string, string>();
        private readonly string scriptDirectory;

        public SourcePathStorage(string scriptDirectory)
        {
            this.scriptDirectory = scriptDirectory;
        }

        public void Add(string fileName, string sourcePath)
        {

        }

        public IReadOnlyDictionary<string, string> GetSourcePaths()
        {
            return paths;
        }
    }
}