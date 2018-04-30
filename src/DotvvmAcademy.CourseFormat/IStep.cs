namespace DotvvmAcademy.CourseFormat
{
    public interface IStep
    {
        CodeTaskId CodeTask { get; }

        StepId Id { get; }

        string SourceText { get; }
    }
}