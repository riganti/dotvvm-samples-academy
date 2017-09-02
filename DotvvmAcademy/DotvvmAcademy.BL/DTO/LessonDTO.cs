using DotvvmAcademy.DAL.Base.Models;

namespace DotvvmAcademy.BL.DTO
{
    public sealed class LessonDTO
    {
        public string Annotation { get; private set; }

        public string ImageUrl { get; private set; }

        public bool IsReady { get; private set; }

        public string Language { get; private set; }

        public string LessonId { get; private set; }

        public string Name { get; private set; }

        public int StepCount { get; private set; }

        public static LessonDTO Create(Lesson lesson)
        {
            var dto = new LessonDTO()
            {
                Annotation = lesson.Annotation,
                ImageUrl = lesson.ImageUrl,
                IsReady = lesson.IsReady,
                Language = lesson.Language,
                Name = lesson.Name,
                LessonId = lesson.LessonId,
                StepCount = lesson.StepPaths.Count
            };
            return dto;
        }
    }
}