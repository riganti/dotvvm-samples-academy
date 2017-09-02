using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.CommonMark.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class StepViewModel : DotvvmAcademyViewModelBase
    {
        private readonly LessonFacade lessonFacade;
        private readonly StepFacade stepFacade;
        private readonly SampleFacade sampleFacade;
        private readonly ValidatorFacade validatorFacade;

        public StepViewModel(LessonFacade lessonFacade, StepFacade stepFacade, 
            SampleFacade sampleFacade, ValidatorFacade validatorFacade)
        {
            this.lessonFacade = lessonFacade;
            this.stepFacade = stepFacade;
            this.sampleFacade = sampleFacade;
            this.validatorFacade = validatorFacade;
        }

        public List<SampleViewModel> Samples { get; set; }

        public List<IComponent> Components { get; set; }

        [FromRoute("Language")]
        public string Language { get; set; }

        [FromRoute("LessonId")]
        public string LessonId { get; set; }

        [FromRoute("StepIndex")]
        public int StepIndex { get; set; }

        public int StepCount { get; set; }

        public override Task Load()
        {
            return Task.CompletedTask;
        }

        public void GoNext()
        {
            if(StepIndex != StepCount - 1)
            {
                var parameters = new { LessonId = LessonId, Language = Language, StepIndex = ++StepIndex };
                Context.RedirectToRoute("Step", parameters);
            }
        }

        public void GoPrevious()
        {
            if (StepIndex != 0)
            {
                var parameters = new { LessonId = LessonId, Language = Language, StepIndex = --StepIndex };
                Context.RedirectToRoute("Step", parameters);
            }
        }
    }
}