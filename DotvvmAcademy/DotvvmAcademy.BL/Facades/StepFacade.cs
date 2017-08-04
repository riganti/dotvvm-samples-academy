using DotvvmAcademy.DAL.Base.Providers;

namespace DotvvmAcademy.BL.Facades
{
    public class StepFacade
    {
        private ILessonProvider lessonProvider;
        private IStepProvider stepProvider;

        public StepFacade(ILessonProvider lessonProvider, IStepProvider stepProvider)
        {
            this.lessonProvider = lessonProvider;
            this.stepProvider = stepProvider;
        }

        public string GetStep(int lessonIndex, string language, int stepIndex)
        {
            var lesson = lessonProvider.Get(lessonIndex, language);
            return stepProvider.Get(lesson, stepIndex);
        }
    }
}