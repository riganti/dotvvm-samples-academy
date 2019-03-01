using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Course : Source
    {
        public Course(ImmutableArray<string> lessons)
            : base("/")
        {
            Lessons = lessons;
        }

        public ImmutableArray<string> Lessons { get; }
    }
}