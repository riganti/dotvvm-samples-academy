using System.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Cache
{
    public class LessonsCache : KeyValueItemCacheBase<Dictionary<string, Dictionary<int, LessonBase>>>
    {
        public LessonsCache(IHostingEnvironment hostingEnvironment)
        {
            var providers = new ILessonProvider[] {
                new EnLessonProvider(hostingEnvironment),
                new CsLessonProvider(hostingEnvironment)
            };

            var dict = providers.ToDictionary(p => p.Language, p => p.CreateLessons());
            SetValue<LessonBase>(dict);
        }

        public Dictionary<int, LessonBase> Get(string lang)
        {
            return GetValue<LessonBase>()[lang];
        }
    }
}