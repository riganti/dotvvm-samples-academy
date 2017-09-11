namespace DotvvmAcademy.BL.Dtos
{
    public abstract class ExerciseBaseDto
    {
        public string CorrectPath { get; internal set; }

        public string IncorrectPath { get; internal set; }

        public CodeLanguageDto CodeLanguage { get; internal set; }

        public string ValidatorId { get; internal set; }
    }
}