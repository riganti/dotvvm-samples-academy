using System;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.StepsBases.Interfaces;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.ViewModels
{
    public class LessonViewModel : SiteViewModel
    {
        protected LessonBase lesson;
        protected int lessonNumber;
        public IStep Step { get; set; }
        public string ErrorMessage { get; private set; }
        public bool ContinueButtonVisible { get; set; } = true;
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
            else if (lessonNumber == 3)
            {
                lesson = new Lesson3();
            }
            else if (lessonNumber == 4)
            {
                lesson = new Lesson4();
            }
            else
            {
                throw new NotSupportedException();
            }

            //todo create cache
            Step = lesson.Steps.First(s => s.StepIndex == CurrentStepNumber);


            AfterLoad();
        }

        protected virtual void AfterLoad()
        {
        }

        public void Continue()
        {
            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());

            if (Step is ValidableStepBase)
            {
                var validableStep = Step as ValidableStepBase;
                ErrorMessage = validableStep.ErrorMessage;
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (CurrentStepNumber < lesson.Steps.Count())
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
            Context.RedirectToRoute("Lesson", new {Step = CurrentStepNumber + 1});
        }
    }
}