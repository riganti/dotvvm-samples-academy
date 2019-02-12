using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonVariant : Source
    {
        public LessonVariant(
            string lessonMoniker,
            string variantMoniker,
            ImmutableArray<string> steps,
            string name,
            LessonStatus status,
            string imageUrl,
            string annotation)
            : base($"/{lessonMoniker}/{variantMoniker}")
        {
            LessonMoniker = lessonMoniker;
            VariantMoniker = variantMoniker;
            Steps = steps;
            Name = name;
            Status = status;
            ImageUrl = imageUrl;
            Annotation = annotation;
        }

        public string Annotation { get; }

        public string ImageUrl { get; }

        public string VariantMoniker { get; }

        public string LessonMoniker { get; }

        public string Name { get; }

        public LessonStatus Status { get; }

        public ImmutableArray<string> Steps { get; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}