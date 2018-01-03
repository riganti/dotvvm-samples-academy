namespace DotvvmAcademy.BL.Dtos
{
    public sealed class DothtmlExerciseStepPartDto : ExerciseStepPartDto
    {
        public DothtmlExerciseStepPartDto(string displayName, string finalPath, string initialPath, string validatorId,
            string viewModelPath, string masterPagePath = null) : base(displayName, finalPath, initialPath, validatorId)
        {
            ViewModelPath = viewModelPath;
            MasterPagePath = masterPagePath;
        }

        public override CodeLanguageDto CodeLanguage => CodeLanguageDto.Dothtml;

        public string MasterPagePath { get; }

        public string ViewModelPath { get; }
    }
}