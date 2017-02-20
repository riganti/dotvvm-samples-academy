using System.Linq;
using DotvvmAcademy.Cache;

namespace DotvvmAcademy.ViewModels
{
    public class EmbeddedViewModel : LessonViewModel
    {
        public EmbeddedViewModel(LessonsCache lessonsCache) : base(lessonsCache)
        {
        }

        protected override void AfterLoad()
        {
            if (CurrentStepNumber == lesson.Steps.Count())
            {
                //this prop changes the view for embedded page (used on dotvvm.com as a sample)
                ContinueButtonVisible = false;
            }
        }

        protected override void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Embedded", new {Step = CurrentStepNumber + 1});
        }
    }
}