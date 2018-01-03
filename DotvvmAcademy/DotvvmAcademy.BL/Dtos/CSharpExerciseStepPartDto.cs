namespace DotvvmAcademy.BL.Dtos
{
    public sealed class CSharpExerciseStepPartDto : ExerciseStepPartDto
    {
        public CSharpExerciseStepPartDto(string displayName, string finalPath, string initialPath, string validatorId, string[] dependencyPaths = null)
            : base(displayName, finalPath, initialPath, validatorId)
        {
            DependencyPaths = dependencyPaths;
        }

        public override CodeLanguageDto CodeLanguage => CodeLanguageDto.CSharp;

        public string[] DependencyPaths { get; }
    }
}