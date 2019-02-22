using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class LessonVariant : Source
    {
        public LessonVariant(
            string lessonMoniker,
            string variantMoniker,
            ImmutableArray<string> steps)
            : base($"/{lessonMoniker}/{variantMoniker}")
        {
            LessonMoniker = lessonMoniker;
            VariantMoniker = variantMoniker;
            Steps = steps;
        }

        public string LessonMoniker { get; }

        public string VariantMoniker { get; }

        public ImmutableArray<string> Steps { get; }

        public string Annotation { get; internal set; }

        public string ImageUrl { get; internal set; }

        public string Name { get; internal set; }

        public LessonStatus Status { get; internal set; }

        public override long GetSize()
        {
            return Annotation?.Length ?? 1;
        }
    }
}