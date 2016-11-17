using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases;

namespace DotvvmAcademy.Lessons
{
    public abstract class LessonBase
    {
        public IEnumerable<StepBase> Steps { get; set; }
    }
}