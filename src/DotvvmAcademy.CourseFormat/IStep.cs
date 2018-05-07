namespace DotvvmAcademy.CourseFormat
{
    public interface IStep
    {
        CodeTaskId CodeTaskId { get; }

        StepId Id { get; }

        string Text { get; }
    }
}