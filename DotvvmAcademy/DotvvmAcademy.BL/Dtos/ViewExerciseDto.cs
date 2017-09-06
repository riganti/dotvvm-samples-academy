using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class ViewExerciseDto : ExerciseDto
    {
        public string MasterPagePath { get; internal set; }
    }
}
