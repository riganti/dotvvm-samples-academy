using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Dtos
{
    public abstract class ExerciseStepPartDto : IStepPartDto
    {
        public ExerciseStepPartDto(string displayName, string finalPath, string initialPath, string validatorId)
        {
            DisplayName = displayName;
            FinalPath = finalPath;
            InitialPath = initialPath;
            ValidatorId = validatorId;
        }

        public abstract CodeLanguageDto CodeLanguage { get; }

        public string DisplayName { get; }

        public string FinalPath { get; }

        public string InitialPath { get; }

        public string ValidatorId { get; }
    }
}
