using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson : Source
    {
        public Lesson(string path, string annotation, ImmutableArray<string> steps) : base(path)
        {
            Annotation = annotation;
            Steps = steps;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string Annotation { get; }

        public string Moniker { get; }

        public ImmutableArray<string> Steps { get; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}