using DotvvmAcademy.BL.CommonMark;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.DAL.Base.Providers;

namespace DotvvmAcademy.BL.Facades
{
    public class StepFacade
    {
        private ILessonProvider lessonProvider;
        private IStepProvider stepProvider;
        private StepParser parser;

        public StepFacade(ILessonProvider lessonProvider, IStepProvider stepProvider, StepParser parser)
        {
            this.lessonProvider = lessonProvider;
            this.stepProvider = stepProvider;
            this.parser = parser;
        }

        public StepDTO GetStep(int lessonIndex, string language, int index)
        {
            var source = GetStepRawSource(lessonIndex, language, index);
            return parser.Parse(lessonIndex, language, index, source);
        }

        public string GetStepRawSource(int lessonIndex, string language, int index)
        {
            var lesson = lessonProvider.Get(lessonIndex, language);
            return stepProvider.Get(lesson, index);
        }
    }
}