namespace DotvvmAcademy.DAL.Entities
{
    public abstract class ExerciseBase
    {
        public string DisplayName { get; set; }

        public string FinalPath { get; set; }

        public string InitialPath { get; set; }

        public string ValidatorId { get; set; }
    }
}