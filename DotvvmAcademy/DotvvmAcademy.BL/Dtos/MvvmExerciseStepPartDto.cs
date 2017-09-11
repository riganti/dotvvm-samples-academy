using System.Collections.Generic;

namespace DotvvmAcademy.BL.Dtos
{
    public sealed class MvvmExerciseStepPartDto : IExerciseStepPartDto
    {
        public StepDto Step { get; set; }

        public ViewExerciseDto ViewExercise { get; set; }

        public ViewModelExerciseDto ViewModelExercise { get; set; }

        public IEnumerable<ExerciseBaseDto> GetExercises()
        {
            yield return ViewExercise;
            yield return ViewModelExercise;
        }

        public sealed class ViewExerciseDto : ExerciseBaseDto
        {
            public ViewExerciseDto()
            {
                CodeLanguage = CodeLanguageDto.Dothtml;
            }

            public string MasterPagePath { get; internal set; }
        }

        public sealed class ViewModelExerciseDto : ExerciseBaseDto
        {
            public ViewModelExerciseDto()
            {
                CodeLanguage = CodeLanguageDto.CSharp;
            }

            public string[] DependencyPaths { get; internal set; }
        }
    }
}