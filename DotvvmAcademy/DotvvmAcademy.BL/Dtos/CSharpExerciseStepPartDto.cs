using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class CSharpExerciseStepPartDto : ExerciseBaseDto, IExerciseStepPartDto
    {
        public CSharpExerciseStepPartDto()
        {
            CodeLanguage = CodeLanguageDto.CSharp;
        }

        public string[] DependencyPaths { get; internal set; }

        public IEnumerable<ExerciseBaseDto> GetExercises()
        {
            yield return this;
        }
    }
}