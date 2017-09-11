namespace DotvvmAcademy.DAL.Entities
{
    public class DothtmlExerciseStepPart : ExerciseBase, IStepPart
    {
        public string ViewModelPath { get; set; }

        public string MasterPagePath { get; set; }
    }
}