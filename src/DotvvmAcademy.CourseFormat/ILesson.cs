using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public interface ILesson
    {
        string Annotation { get; }

        LessonId Id { get; }

        ImmutableDictionary<string, StepId> Steps { get; }
    }
}