using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class MvvmExerciseStepPartDto : IExerciseStepPartDto
    {
        public ViewExerciseDto ViewExercise { get; set; }

        public ViewModelExerciseDto ViewModelExercise { get; set; }

        public StepDto Step { get; set; }

        public IEnumerable<ExerciseDto> GetExercises()
        {
            yield return ViewExercise;
            yield return ViewModelExercise;
        }
    }
}