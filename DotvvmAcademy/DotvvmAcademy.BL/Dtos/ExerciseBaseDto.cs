namespace DotvvmAcademy.BL.Dtos
{
    public abstract class ExerciseBaseDto
    {
        public CodeLanguageDto CodeLanguage { get; internal set; }

        public string DisplayName { get; set; }

        public string FinalPath { get; internal set; }

        public string InitialPath { get; internal set; }

        public string ValidatorId { get; internal set; }
    }
}