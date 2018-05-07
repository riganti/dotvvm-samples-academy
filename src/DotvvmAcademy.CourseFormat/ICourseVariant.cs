using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public interface ICourseVariant
    {
        string Annotation { get; }

        CourseVariantId Id { get; }

        ImmutableArray<LessonId> Lessons { get; }
    }
}