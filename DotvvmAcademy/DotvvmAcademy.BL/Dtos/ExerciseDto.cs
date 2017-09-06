namespace DotvvmAcademy.BL.Dtos
{
    public class ExerciseDto
    {
        public string Correct { get; internal set; }

        public string CorrectPath { get; internal set; }

        public string Incorrect { get; internal set; }

        public string IncorrectPath { get; internal set; }

        public CodeLanguageDto CodeLanguage { get; internal set; }

        public string ValidatorId { get; internal set; }
    }
}