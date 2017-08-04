using DotvvmAcademy.DAL.Base.Entities;
using System.Collections.Concurrent;

namespace DotvvmAcademy.DAL.Base.Cache
{
    public class StepCache : IStepCache
    {
        private static ConcurrentDictionary<StepIdentifier, string> dictionary = new ConcurrentDictionary<StepIdentifier, string>();

        public void Add(StepIdentifier identifier, string step)
        {
            dictionary.TryAdd(identifier, step);
        }

        public void Add(string step, Lesson lesson, int stepIndex)
        {
            var identifier = new StepIdentifier(lesson.Index, lesson.Language, stepIndex);
            Add(identifier, step);
        }

        public void Add(string step, LessonIdentifier lessonIdentifier, int stepIndex)
        {
            var identifier = new StepIdentifier(lessonIdentifier.Index, lessonIdentifier.Language, stepIndex);
        }

        public string Get(StepIdentifier identifier)
        {
            dictionary.TryGetValue(identifier, out string step);
            return step;
        }
    }
}