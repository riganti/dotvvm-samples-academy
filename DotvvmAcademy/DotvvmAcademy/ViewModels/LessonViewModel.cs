using System;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Cache;
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

        //public List<IStep> Steps { get; set; }

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

            var cache = new LessonsCache();
            var lessons = cache.Get();


            if (lessonNumber == 1)
            {
                lesson = lessons.First(l => l.Key == 1).Value;
            }
            else if (lessonNumber == 2)
            {
                lesson = lessons.First(l => l.Key == 2).Value;
            }
            else if (lessonNumber == 3)
            {
                lesson = lessons.First(l => l.Key == 3).Value;
            }
            else if (lessonNumber == 4)
            {
                lesson = lessons.First(l => l.Key == 4).Value;
            }
            else
            {
                throw new NotSupportedException();
            }
            Step = lesson.Steps.First(s => s.StepIndex == CurrentStepNumber);
            AfterLoad();
        }

        protected virtual void AfterLoad()
        {
        }

        public void Continue()
        {
            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());

            var step = Step as ValidableStepBase;
            if (step != null)
            {
                var validableStep = step;
                if (Context.IsPostBack)
                {
                   ErrorMessage = validableStep.Validate();
                }
               
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