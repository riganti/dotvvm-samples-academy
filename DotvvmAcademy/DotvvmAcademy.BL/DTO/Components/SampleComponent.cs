namespace DotvvmAcademy.BL.DTO.Components
{
    public class SampleComponent : SourceComponent
    {
        public SampleComponent(int lessonIndex, string language, int stepIndex) : base(lessonIndex, language, stepIndex)
        {
        }

        public string CorrectPath { get; internal set; }

        public string IncorrectPath { get; internal set; }

        public string Validator { get; internal set; }
    }
}