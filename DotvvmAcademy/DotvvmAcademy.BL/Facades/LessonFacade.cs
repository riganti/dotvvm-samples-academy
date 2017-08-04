using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Facades
{
    public class LessonFacade
    {
        private ILessonProvider lessonProvider;

        public LessonFacade(ILessonProvider lessonProvider)
        {
            this.lessonProvider = lessonProvider;
        }

        public IEnumerable<LessonDTO> GetAllLessons(int? lessonIndex = null, string language = null)
        {
            foreach (var lesson in lessonProvider.GetQueryable(lessonIndex, language))
            {
                yield return LessonDTO.Create(lesson);
            }
        }

        public int GetLessonStepCount(int lessonIndex, string language)
        {
            var lesson = lessonProvider.Get(lessonIndex, language);
            return lesson.Steps.Count;
        }
    }
}
