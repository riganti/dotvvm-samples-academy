using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class StepViewModel : DotvvmAcademyViewModelBase
    {
        private readonly ExerciseFacade exerciseFacade;
        private readonly SampleFacade sampleFacade;
        private readonly ValidatorFacade validatorFacade;
        private readonly Func<ExerciseViewModel> exerciseFactory;
        private readonly LessonFacade lessonFacade;
        private readonly StepFacade stepFacade;

        public StepViewModel(LessonFacade lessonFacade, StepFacade stepFacade, ExerciseFacade exerciseFacade, 
            SampleFacade sampleFacade, ValidatorFacade validatorFacade, Func<ExerciseViewModel> exerciseFactory)
        {
            this.lessonFacade = lessonFacade;
            this.stepFacade = stepFacade;
            this.exerciseFacade = exerciseFacade;
            this.sampleFacade = sampleFacade;
            this.validatorFacade = validatorFacade;
            this.exerciseFactory = exerciseFactory;
        }

        public bool CanGoNext { get; set; }

        public bool CanGoPrevious { get; set; }

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

        public override async Task Init()
        {
            Lesson = await lessonFacade.GetOverview(Moniker, Language);
            Step = await stepFacade.GetStep(Lesson, StepIndex);
            CanGoNext = StepIndex != Lesson.StepCount - 1;
            CanGoPrevious = StepIndex != 0;
        }

        public override Task Load()
        {
            var exerciseDtos = exerciseFacade.GetExercises(Step).ToArray();
            if (!Context.IsPostBack)
            {
                Exercises = exerciseDtos.Select((dto, i) =>
                {
                    var exercise = exerciseFactory();
                    exercise.Dto = dto;
                    exercise.Index = i;
                    return exercise;
                }).ToList();
                return Task.CompletedTask;
            }
            foreach (var exercise in Exercises)
            {
                exercise.Dto = exerciseDtos[exercise.Index];
                exercise.InjectServices(validatorFacade, sampleFacade);
            }

            return Task.CompletedTask;
        }
    }
}