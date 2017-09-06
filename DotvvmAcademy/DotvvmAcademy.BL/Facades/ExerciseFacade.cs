using DotvvmAcademy.BL.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Facades
{
    public class ExerciseFacade : IFacade
    {
        public IEnumerable<ExerciseDto> GetExercises(StepDto stepDto)
        {
            foreach (var part in stepDto.Source.OfType<IExerciseStepPartDto>())
            {
                foreach (var exercise in part.GetExercises())
                {
                    yield return exercise;
                }
            }
        }
    }
}