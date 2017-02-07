using System.Collections.Generic;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;

namespace DotvvmAcademy.Cache
{
    public class LessonsCache : KeyValueItemCacheBase<Dictionary<int, LessonBase>>
    {
        public LessonsCache()
        {
            var alp = new AllLessonProvider();
            Set(alp.CreateLessons());
        }

        public Dictionary<int, LessonBase> Get()
        {
            return GetValue<LessonBase>();
        }

        public void Set(Dictionary<int, LessonBase> value)
        {
            SetValue<LessonBase>(value);
        }
    }
}