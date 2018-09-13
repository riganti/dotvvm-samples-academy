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

        public ImmutableArray<string> Dependencies { get; }

        public string Path { get; }
    }
}