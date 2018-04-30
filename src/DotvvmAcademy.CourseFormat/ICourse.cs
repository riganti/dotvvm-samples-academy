using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public interface ICourse
    {
        string Culture { get; }

        CourseId Id { get; }

        ImmutableArray<LessonId> Lessons { get; set; }
    }
}