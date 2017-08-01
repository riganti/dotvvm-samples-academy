using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Steps
{
    public class InfoStep : IStep
    {
        public string Description { get; set; }

        public int StepIndex { get; set; }

        public string Title { get; set; }
    }
}