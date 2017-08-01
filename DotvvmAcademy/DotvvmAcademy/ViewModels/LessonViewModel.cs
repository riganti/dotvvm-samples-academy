using System;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Cache;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.StepsBases.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class LessonViewModel : SiteViewModel
    {
        protected LessonBase lesson;
        protected int lessonNumber;
        private readonly LessonsCache lessonsCache;

        public LessonViewModel(LessonsCache lessonsCache)
        {
            this.lessonsCache = lessonsCache;
        }

        public bool ContinueButtonVisible { get; set; } = true;

        public string ErrorMessage { get; private set; }

        public IStep Step { get; set; }

        protected int CurrentStepNumber { get; set; }

        protected int NextStepNumber => CurrentStepNumber + 1;

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
                    storage.UpdateLessonLastStep(lessonNumber, NextStepNumber);
                    RedirectToNextLesson();
                }
                else
                {
                    storage.UpdateLessonLastStep(lessonNumber, LessonProgressStorage.FinishedLessonStepNumber);
                    Context.RedirectToRoute("Default");
                }
            }
        }

        public override Task Init()
        {
            LoadStep();
            return base.Init();
        }

        protected virtual void AfterLoad()
        {
        }

        protected virtual void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Lesson", new { Step = NextStepNumber });
        }

        private void LoadStep()
        {
            lessonNumber = Convert.ToInt32(Context.Parameters["Lesson"]);
            CurrentStepNumber = Convert.ToInt32(Context.Parameters["Step"]);

            var lessons = lessonsCache.Get();

            lesson = lessons.First(l => l.Key == lessonNumber).Value;
            if (lesson == null)
            {
                throw new NotSupportedException();
            }

            Step = lesson.Steps.First(s => s.StepIndex == CurrentStepNumber);
            AfterLoad();
        }
    }
}