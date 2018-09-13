using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson : Source
    {
        public Lesson(string path, string annotation, ImmutableArray<string> steps, string name, string imageUrl) : base(path)
        {
            Annotation = annotation;
            Steps = steps;
            Name = name;
            ImageUrl = imageUrl;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string Annotation { get; }

        public string ImageUrl { get; }

        public string Moniker { get; }

        public string Name { get; }

        public ImmutableArray<string> Steps { get; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}