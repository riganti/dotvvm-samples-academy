using System.Collections.Generic;

namespace DotvvmAcademy.CourseFormat
{
    public class SourcePathStorage
    {
        private readonly string scriptDirectory;
        private readonly HashSet<string> paths = new HashSet<string>();

        public SourcePathStorage(string scriptDirectory)
        {
            this.scriptDirectory = scriptDirectory;
        }

        public void Add(string sourcePath)
        {
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            paths.Add(absolutePath);
        }

        public IEnumerable<string> GetSourcePaths()
        {
            return paths;
        }
    }
}