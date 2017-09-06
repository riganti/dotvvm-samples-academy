using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.DAL.Entities
{
    public class ExerciseStepPartBase : IStepPart
    {
        public string CorrectPath { get; set; }

        public string IncorrectPath { get; set; }

        public string ValidatorId { get; set; }
    }
}
