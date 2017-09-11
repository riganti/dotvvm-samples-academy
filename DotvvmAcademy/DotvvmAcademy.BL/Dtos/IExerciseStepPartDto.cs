using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Dtos
{
    public interface IExerciseStepPartDto : IStepPartDto
    {
        IEnumerable<ExerciseBaseDto> GetExercises();
    }
}
