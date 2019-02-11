using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Lesson : Source
    {
        public Lesson(string path, string annotation, ImmutableArray<string> steps, string name, string imageUrl, LessonStatus status)
            : base(path)
        {
            Annotation = annotation;
            Steps = steps;
            Name = name;
            ImageUrl = imageUrl;
            Status = status;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public string Annotation { get; }

        public string ImageUrl { get; }

        public string Moniker { get; }

        public string Name { get; }

        public LessonStatus Status { get; }

        public ImmutableArray<string> Steps { get; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}