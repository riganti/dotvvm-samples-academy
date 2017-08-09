using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Validation;

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

        public string Code { get; set; }

        [Bind(Direction.None)]
        public SampleCodeLanguage CodeLanguage { get; internal set; }

        [Bind(Direction.None)]
        public string CorrectCode { get; internal set; }

        [Bind(Direction.None)]
        public string IncorrectCode { get; internal set; }

        [Bind(Direction.None)]
        public string Language { get; }

        [Bind(Direction.None)]
        public int LessonIndex { get; }

        [Bind(Direction.None)]
        public int StepIndex { get; }

        [Bind(Direction.None)]
        public ValidationDelegate Validate { get; internal set; }
    }
}