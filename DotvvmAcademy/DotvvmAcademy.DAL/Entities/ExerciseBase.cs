namespace DotvvmAcademy.DAL.Entities
{
    public abstract class ExerciseBase
    {
        public string CorrectPath { get; set; }

        public string IncorrectPath { get; set; }

        public string ValidatorId { get; set; }
    }
}