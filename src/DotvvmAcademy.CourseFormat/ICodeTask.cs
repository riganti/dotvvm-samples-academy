namespace DotvvmAcademy.CourseFormat
{
    public interface ICodeTask
    {
        string Code { get; }

        CodeTaskId Id { get; }

        string ValidationScript { get; }
    }
}