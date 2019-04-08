using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Course
    {
        private readonly ImmutableDictionary<string, Lesson> associativeLessons;

        public Course(string path, IEnumerable<Lesson> lessons)
        {
            Path = path;
            Lessons = lessons.ToImmutableArray();
            associativeLessons = lessons.ToImmutableDictionary(l => l.Moniker);
        }

        public string Path { get; set; }

        public ImmutableArray<Lesson> Lessons { get; }

        public Lesson GetLesson(string moniker)
        {
            if(associativeLessons.TryGetValue(moniker, out var lesson))
            {
                return lesson;
            }
            return null;
        }
    }
}