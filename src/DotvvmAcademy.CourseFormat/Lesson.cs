using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.CourseFormat
{
    internal class Lesson : ILesson
    {
        public Lesson(LessonId id)
        {
            Id = id;
        }

        public string Annotation { get; set; }

        public LessonId Id { get; }

        public ImmutableArray<StepId> Steps { get; set; }
    }
}