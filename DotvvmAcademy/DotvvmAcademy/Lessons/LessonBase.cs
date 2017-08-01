using DotvvmAcademy.Steps.StepsBases.Interfaces;
using System.Collections.Generic;

namespace DotvvmAcademy.Lessons
{
    public abstract class LessonBase
    {
        public IEnumerable<IStep> Steps { get; set; }
    }
}