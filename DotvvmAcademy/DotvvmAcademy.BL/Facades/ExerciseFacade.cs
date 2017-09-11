using DotvvmAcademy.BL.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Facades
{
    public class ExerciseFacade : IFacade
    {
        public IEnumerable<ExerciseBaseDto> GetExercises(StepDto stepDto)
        {
            var parts = stepDto?.Source.OfType<IExerciseStepPartDto>() ?? Enumerable.Empty<IExerciseStepPartDto>();
            foreach (var part in parts)
            {
                foreach (var exercise in part.GetExercises())
                {
                    yield return exercise;
                }
            }
        }
    }
}