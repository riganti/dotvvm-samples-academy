using DotvvmAcademy.DAL.Base.Providers;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Facades
{
    public class SampleFacade
    {
        private ISampleProvider sampleProvider;
        private ILessonProvider lessonProvider;

        public SampleFacade(ILessonProvider lessonProvider, ISampleProvider sampleProvider)
        {
            this.lessonProvider = lessonProvider;
            this.sampleProvider = sampleProvider;
        }

        public string GetSample(int lessonIndex, string lessonLanguage, string path)
        {
            var lesson = lessonProvider.Get(lessonIndex, lessonLanguage);
            return sampleProvider.Get(lesson, path);
        }

        public IEnumerable<string> GetSample(int lessonIndex, string lessonLanguage, IEnumerable<string> paths)
        {
            var lesson = lessonProvider.Get(lessonIndex, lessonLanguage);
            return sampleProvider.GetQueryable(lesson, paths).ToList();
        }
    }
}