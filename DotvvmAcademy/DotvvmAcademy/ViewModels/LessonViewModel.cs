using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
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
        private readonly LessonFacade lessonFacade;
        private readonly SampleFacade sampleFacade;
        private readonly StepFacade stepFacade;
        private readonly ValidatorFacade validatorFacade;
        private readonly ValidatorsBuilder validatorsBuilder;

        public LessonViewModel(LessonFacade lessonFacade, StepFacade stepFacade, SampleFacade sampleFacade, ValidatorFacade validatorFacade, ValidatorsBuilder validatorsBuilder)
        {
            this.lessonFacade = lessonFacade;
            this.stepFacade = stepFacade;
            this.sampleFacade = sampleFacade;
            this.validatorFacade = validatorFacade;
            this.validatorsBuilder = validatorsBuilder;
        }

        public bool ContinueButtonVisible { get; set; } = true;

        public bool IsValid { get; set; } = true;

        public string Language { get; set; } = "en";

        public int LessonIndex { get; private set; }

        public int LessonStepCount { get; private set; }

        public List<SampleViewModel> Samples { get; set; } = new List<SampleViewModel>();

        [Bind(Direction.None)]
        public List<SampleDTO> SampleDTOs { get; set; } = new List<SampleDTO>();

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
            return base.Init();
        }

        public override Task Load() {
            ProcessStepSource();
            AfterLoad();
            return Task.FromResult(0);
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
            var sampleComponents = Step.Source.OfType<SampleComponent>().ToList();
            for (int i = 0; i < sampleComponents.Count; i++)
            {
                var component = sampleComponents[i];
                var dto = sampleFacade.GetSample(component);
                if(!Context.IsPostBack)
                {
                    var sampleViewModel = new SampleViewModel
                    {
                        ValidatorFacade = validatorFacade,
                        DTO = dto,
                        ValidatorsBuilder = validatorsBuilder,
                    };
                    Samples.Add(sampleViewModel);
                }
                else
                {
                    Samples[i].DTO = dto;
                    Samples[i].ValidatorFacade = validatorFacade;
                    Samples[i].ValidatorsBuilder = validatorsBuilder;
                }
            }
        }

        private void ProcessStepSource()
        {
            ProcessSamples();
        }
    }
}