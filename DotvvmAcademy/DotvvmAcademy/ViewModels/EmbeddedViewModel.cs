namespace DotvvmAcademy.ViewModels
{
    public class EmbeddedViewModel : LessonViewModel
    {
        protected override void AfterLoad()
        {
            if (CurrentStepNumber == lesson.GetAllSteps().Length)
            {
                //TODO: This part of code is only for sample view on dotvvm website - Make this code as a control usable for embending.
                ContinueButtonVisible = false;
            }
        }
        protected override void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Embedded", new { Step = CurrentStepNumber + 1 });
        }
    }
}