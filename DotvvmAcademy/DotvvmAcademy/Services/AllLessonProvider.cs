using System.Collections.Generic;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.Services
{
    public class AllLessonProvider
    {
        public Dictionary<int, LessonBase> CreateLessons()
        {
            var lessons = new Dictionary<int, LessonBase>
            {
                {1, new Lesson1()},
                {2, new Lesson2()},
                {3, new Lesson3()},
                {4, new Lesson4()}
            };
            return lessons;
        }
    }
}