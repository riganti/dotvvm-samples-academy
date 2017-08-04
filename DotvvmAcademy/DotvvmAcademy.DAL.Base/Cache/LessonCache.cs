using DotvvmAcademy.DAL.Base.Entities;
using System.Collections.Concurrent;

namespace DotvvmAcademy.DAL.Base.Cache
{
    public class LessonCache : ILessonCache
    {
        private static ConcurrentDictionary<LessonIdentifier, Lesson> dictionary = new ConcurrentDictionary<LessonIdentifier, Lesson>();

        public void Add(Lesson lesson)
        {
            var identifier = new LessonIdentifier(lesson.Index, lesson.Language);
            Add(identifier, lesson);
        }

        public void Add(LessonIdentifier identifier, Lesson lesson)
        {
            dictionary.TryAdd(identifier, lesson);
        }

        public Lesson Get(LessonIdentifier identifier)
        {
            dictionary.TryGetValue(identifier, out Lesson lesson);
            return lesson;
        }
    }
}