using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class DothtmlExerciseStepPartDto : ExerciseBaseDto, IExerciseStepPartDto
    {
        public DothtmlExerciseStepPartDto()
        {
            CodeLanguage = CodeLanguageDto.Dothtml;
        }

        public string MasterPagePath { get; internal set; }

        public string ViewModelPath { get; internal set; }

        public IEnumerable<ExerciseBaseDto> GetExercises()
        {
            yield return this;
        }
    }
}