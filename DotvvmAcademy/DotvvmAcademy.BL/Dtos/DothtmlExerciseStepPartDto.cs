using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class DothtmlExerciseStepPartDto : ExerciseDto, IExerciseStepPartDto
    {
        public string MasterPagePath { get; internal set; }

        public string ViewModelPath { get; internal set; }

        public IEnumerable<ExerciseDto> GetExercises()
        {
            yield return this;
        }
    }
}