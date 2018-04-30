using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public interface ILesson
    {
        LessonId Id { get; set; }

        ImmutableArray<StepId> Steps { get; }
    }
}