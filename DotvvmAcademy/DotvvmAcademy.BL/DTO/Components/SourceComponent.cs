namespace DotvvmAcademy.BL.DTO.Components
{
    public abstract class SourceComponent
    {
        public SourceComponent(int lessonIndex, string language, int stepIndex)
        {
            LessonIndex = lessonIndex;
            Language = language;
            StepIndex = stepIndex;
        }

        public string Language { get; }

        public int LessonIndex { get; }

        public int StepIndex { get; }
    }
}