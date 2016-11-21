using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Steps
{
    public class InfoStep : IStep
    {
        public int StepIndex { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}