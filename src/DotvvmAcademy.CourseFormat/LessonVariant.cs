using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonVariant
    {
        public LessonVariant(
            string path, 
            string moniker, 
            string annotationPath, 
            string imageUrl,
            string name,
            LessonStatus status,
            ImmutableDictionary<string, Step> steps)
        {
            Path = path;
            Moniker = moniker;
            AnnotationPath = annotationPath;
            ImageUrl = imageUrl;
            Name = name;
            Status = status;
            Steps = steps;
        }

        public string Path { get; }

        public string Moniker { get; }

        public string AnnotationPath { get; }

        public string ImageUrl { get; }

        public string Name { get; }

        public LessonStatus Status { get; }

        public ImmutableDictionary<string, Step> Steps { get; }
    }
}