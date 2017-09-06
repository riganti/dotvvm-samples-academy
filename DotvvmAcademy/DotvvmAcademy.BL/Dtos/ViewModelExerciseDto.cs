namespace DotvvmAcademy.BL.Dtos
{
    public sealed class ViewModelExerciseDto : ExerciseDto
    {
        public string[] DependencyPaths { get; internal set; }
    }
}