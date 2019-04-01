using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask
    {
        public CodeTask(string path, string correctPath, string defaultPath, ImmutableArray<string> dependencies)
        {
            Path = path;
            CorrectPath = correctPath;
            DefaultPath = defaultPath;
            Dependencies = dependencies;
        }

        public string Path { get; }

        public string CorrectPath { get; }

        public string DefaultPath { get; }

        public ImmutableArray<string> Dependencies { get; }
    }
}