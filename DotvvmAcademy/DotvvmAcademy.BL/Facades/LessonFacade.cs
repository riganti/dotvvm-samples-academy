using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.DAL.Base.Models;
using DotvvmAcademy.DAL.Base.Providers;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Facades
{
    public class LessonFacade
    {
        private ILessonProvider lessonProvider;

        public LessonFacade(ILessonProvider lessonProvider)
        {
            this.lessonProvider = lessonProvider;
        }

        public IEnumerable<LessonDTO> GetAllLessons(string lessonId = null, string language = null)
        {
            var filter = new LessonFilter { Language = language, LessonId = lessonId };
            foreach (var lessonIdentifier in lessonProvider.GetQueryable(filter))
            {
                var lesson = lessonProvider.Get(lessonIdentifier);
                yield return LessonDTO.Create(lesson);
            }
        }

        public int GetLessonStepCount(string lessonId, string language)
        {
            var lesson = lessonProvider.Get(new LessonIdentifier(lessonId, language));
            return lesson.StepPaths.Count;
        }
    }
}