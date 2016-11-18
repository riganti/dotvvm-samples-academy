namespace DotvvmAcademy.Steps.StepsBases
{
    public interface IStep
    {
        int StepIndex { get; set; }
        string Description { get; set; }
        string Title { get; set; }
    }
}