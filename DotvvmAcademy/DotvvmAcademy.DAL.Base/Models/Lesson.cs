using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public class Lesson
    {
        public Lesson(string lessonId, string language)
        {
            LessonId = lessonId;
            Language = language;
        }

        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        public string Language { get; }

        public string LessonId { get; }

        public string Name { get; set; }

        public string Path { get; set; }

        public List<string> StepPaths { get; set; }
    }
}