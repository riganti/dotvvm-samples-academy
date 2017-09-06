using AutoMapper;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class StepViewModel : DotvvmAcademyViewModelBase
    {
        private readonly ExerciseFacade exerciseFacade;
        private readonly LessonFacade lessonFacade;
        private readonly StepFacade stepFacade;
        private readonly ValidatorFacade validatorFacade;

        public StepViewModel(LessonFacade lessonFacade, StepFacade stepFacade, ValidatorFacade validatorFacade, ExerciseFacade exerciseFacade)
        {
            this.lessonFacade = lessonFacade;
            this.stepFacade = stepFacade;
            this.validatorFacade = validatorFacade;
            this.exerciseFacade = exerciseFacade;
        }

        public List<ExerciseViewModel> Exercises { get; set; }

        [FromRoute("Language")]
        public string Language { get; set; }

        public LessonOverviewDto Lesson { get; set; }

        [FromRoute("Moniker")]
        public string Moniker { get; set; }

        [Bind(Direction.None)]
        public StepDto Step { get; set; }

        [FromRoute("StepIndex")]
        public int StepIndex { get; set; }

        public void GoNext()
        {
            if (StepIndex != Lesson.StepCount - 1)
            {
                var parameters = new { Moniker = Moniker, Language = Language, StepIndex = ++StepIndex };
                Context.RedirectToRoute("Step", parameters);
            }
        }

        public void GoPrevious()
        {
            if (StepIndex != 0)
            {
                var parameters = new { Moniker = Moniker, Language = Language, StepIndex = --StepIndex };
                Context.RedirectToRoute("Step", parameters);
            }
        }

        public override async Task Init()
        {
            Lesson = await lessonFacade.GetOverview(Moniker, Language);
            Step = await stepFacade.GetStep(Lesson, StepIndex);

            if (!Context.IsPostBack)
            {
                Exercises = exerciseFacade.GetExercises(Step).Select(e => Mapper.Map<ExerciseDto, ExerciseViewModel>(e)).ToList();
            }
        }

        public override Task Load()
        {
            foreach (var exercise in Exercises)
            {
                exercise.ValidatorFacade = validatorFacade;
            }

            return Task.CompletedTask;
        }
    }
}