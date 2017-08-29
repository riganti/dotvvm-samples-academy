namespace DotvvmAcademy.BL.DTO
{
    public sealed class SampleDTO
    {
        public SampleDTO(int lessonIndex, string language, int stepIndex)
        {
            LessonIndex = lessonIndex;
            Language = language;
            StepIndex = stepIndex;
        }

        public SampleCodeLanguage CodeLanguage { get; internal set; }

        public string CorrectCode { get; internal set; }

        public string IncorrectCode { get; internal set; }

        public string Language { get; }

        public int LessonIndex { get; }

        public int StepIndex { get; }

        public string ValidatorKey { get; internal set; }
    }
}