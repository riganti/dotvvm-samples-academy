using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.DAL.Entities
{
    public class MvvmExerciseStepPart
    {
        public string CorrectViewPath { get; set; }

        public string CorrectViewModelPath { get; set; }

        public string IncorrectViewPath { get; set; }

        public string IncorrectViewModelPath { get; set; }

        public string ViewValidatorId { get; set; }

        public string ViewModelValidatorId { get; set; }

        public string MasterPagePath { get; set; }

        public string[] ViewModelDependencies { get; set; }
    }
}
