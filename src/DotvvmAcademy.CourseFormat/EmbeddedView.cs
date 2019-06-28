using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class EmbeddedView
    {
        public EmbeddedView(string path, ImmutableArray<string> dependencies)
        {
            Path = path;
            Dependencies = dependencies;
        }

        public string Path { get; }

        public ImmutableArray<string> Dependencies { get; }
    }
}