using DotvvmAcademy.BL.Facades;

namespace DotvvmAcademy.ViewModels
{
    public class EmbeddedViewModel : LessonViewModel
    {
        public EmbeddedViewModel(LessonFacade lessonFacade, StepFacade stepFacade) : base(lessonFacade, stepFacade)
        {
        }

        protected override void AfterLoad()
        {
            if (StepIndex == LessonStepCount - 1)
            {
                //this prop changes the view for embedded page (used on dotvvm.com as a sample)
                ContinueButtonVisible = false;
            }
        }

        protected override void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Embedded", new { Step = StepIndex + 2 });
        }
    }
}