using System.Diagnostics;

namespace DotvvmAcademy.CourseFormat
{
    internal class Step : IStep
    {
        public Step(StepId id)
        {
            Id = id;
        }

        public CodeTaskId CodeTaskId { get; set; }

        public StepId Id { get; }

        public string Text { get; set; }
    }
}