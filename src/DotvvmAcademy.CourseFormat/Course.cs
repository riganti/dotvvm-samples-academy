using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Course
    {
        public Course(string path, ImmutableDictionary<string, Lesson> lessons)
        {
            Path = path;
            Lessons = lessons;
        }

        public string Path { get; set; }

        public ImmutableDictionary<string, Lesson> Lessons { get; }
    }
}