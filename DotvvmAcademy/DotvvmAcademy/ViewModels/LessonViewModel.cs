using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class LessonViewModel : DotvvmAcademyViewModelBase
    {
        private LessonFacade lessonFacade;
        private SampleFacade sampleFacade;
        private StepFacade stepFacade;

        public LessonViewModel(LessonFacade lessonFacade, StepFacade stepFacade, SampleFacade sampleFacade)
        {
            this.lessonFacade = lessonFacade;
            this.stepFacade = stepFacade;
            this.sampleFacade = sampleFacade;
        }

        public bool ContinueButtonVisible { get; set; } = true;

        public bool IsValid { get; set; } = true;

        public string Language { get; set; } = "en";

        public int LessonIndex { get; private set; }

        public int LessonStepCount { get; private set; }

        public List<SampleDTO> Samples { get; set; } = new List<SampleDTO>();

        [Bind(Direction.None)]
        public StepDTO Step { get; set; }

        public int StepIndex { get; private set; }

        public void Continue()
        {
            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());

            if (IsValid)
            {
                if (StepIndex < LessonStepCount - 1)
                {
                    storage.UpdateLessonLastStep(LessonIndex + 1, StepIndex + 2);
                    RedirectToNextLesson();
                }
                else
                {
                    storage.UpdateLessonLastStep(LessonIndex + 1, LessonProgressStorage.FinishedLessonStepNumber);
                    Context.RedirectToRoute("Default");
                }
            }
        }

        public override Task Init()
        {
            LessonIndex = Convert.ToInt32(Context.Parameters["Lesson"]) - 1;
            StepIndex = Convert.ToInt32(Context.Parameters["Step"]) - 1;
            Step = stepFacade.GetStep(LessonIndex, Language, StepIndex);
            LessonStepCount = lessonFacade.GetLessonStepCount(LessonIndex, Language);
            ProcessStepSource();

            AfterLoad();

            return base.Init();
        }

        protected virtual void AfterLoad()
        {
        }

        protected virtual void RedirectToNextLesson()
        {
            Context.RedirectToRoute("Lesson", new { Step = StepIndex + 2 });
        }

        private void ProcessSamples()
        {
            foreach (var sample in Step.Source.OfType<SampleComponent>())
            {
                Samples.Add(sampleFacade.GetSample(sample));
            }
        }

        private void ProcessStepSource()
        {
            ProcessSamples();
        }
    }
}