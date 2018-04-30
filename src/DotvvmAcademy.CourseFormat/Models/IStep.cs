namespace DotvvmAcademy.CourseFormat.Models
{
    public interface IStep
    {
        ICodeTask CodeTask { get; }

        string SourceText { get; }
    }
}