using DotVVM.Framework.Hosting;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using DotvvmAcademy.Steps;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class LessonViewModel : SiteViewModel
    {
        public StepBase Step { get; set; }
        public string ErrorMessage { get; private set; }
        public bool ContinueButtonVisible { get; set; } = true;

        protected LessonBase lesson;
        protected int lessonNumber;
        protected int CurrentStepNumber { get; set; }

        public override Task Init()
        {
            LoadStep();

            return base.Init();
        }

        private void LoadStep()
        {
            lessonNumber = Convert.ToInt32(Context.Parameters["Lesson"]);
            CurrentStepNumber = Convert.ToInt32(Context.Parameters["Step"]);

            if (lessonNumber == 1)
            {
                lesson = new Lesson1();
            }
            else if (lessonNumber == 2)
            {
                lesson = new Lesson2();
            }
            else
            {
                throw new NotSupportedException();
            }
            Step = lesson.GetAllSteps()[CurrentStepNumber - 1];
            AfterLoad();
           
        }

        protected virtual void AfterLoad()
        {
        }

        public void Continue()
        {
            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());

            ErrorMessage = Step.ErrorMessage;

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (CurrentStepNumber < lesson.GetAllSteps().Length)
                {
                    storage.UpdateLessonLastStep(lessonNumber, CurrentStepNumber + 1);
                    RedirectToNextLesson();
                }
                else
                {
                    storage.UpdateLessonLastStep(lessonNumber, LessonProgressStorage.FinishedLessonStepNumber);
                    Context.RedirectToRoute("Default");
                }
            }
        }

        protected virtual void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Lesson", new { Step = CurrentStepNumber + 1 });
        }
    }
}