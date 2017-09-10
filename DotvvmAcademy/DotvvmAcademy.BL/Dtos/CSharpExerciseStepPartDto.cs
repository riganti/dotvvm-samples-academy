using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class CSharpExerciseStepPartDto : ExerciseDto, IExerciseStepPartDto
    {
        public string[] DependencyPaths { get; internal set; }

        public IEnumerable<ExerciseDto> GetExercises()
        {
            yield return this;
        }
    }
}