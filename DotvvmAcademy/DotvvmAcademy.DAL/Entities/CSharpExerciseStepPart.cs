namespace DotvvmAcademy.DAL.Entities
{
    public class CSharpExerciseStepPart : ExerciseBase, IStepPart
    {
        public string[] DependencyPaths { get; set; }
    }
}