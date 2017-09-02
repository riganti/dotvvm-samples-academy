using System.Collections.Generic;

namespace DotvvmAcademy.BL.DTO
{
    public sealed class SampleDTO
    {
        public SampleDTO(string lessonId, string language, int stepIndex)
        {
            LessonId = lessonId;
            Language = language;
            StepIndex = stepIndex;
        }

        public SampleCodeLanguage CodeLanguage { get; internal set; }

        public IEnumerable<string> Dependencies { get; internal set; }

        public string CorrectCode { get; internal set; }

        public string IncorrectCode { get; internal set; }

        public string Language { get; }

        public string LessonId { get; }

        public int StepIndex { get; }

        public string ValidatorKey { get; internal set; }
    }
}