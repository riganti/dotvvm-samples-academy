namespace DotvvmAcademy.ViewModels
{
    public class EmbeddedViewModel : LessonViewModel
    {
        protected override void AfterLoad()
        {
            if (CurrentStepNumber == lesson.GetAllSteps().Length)
            {
                //this prop changes the view for embedded page (used on dotvvm.com as a sample)
                ContinueButtonVisible = false;
            }
        }
        protected override void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Embedded", new { Step = CurrentStepNumber + 1 });
        }
    }
}