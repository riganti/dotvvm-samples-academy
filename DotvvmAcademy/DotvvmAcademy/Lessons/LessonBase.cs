using DotvvmAcademy.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Lessons
{
    public abstract class LessonBase
    {
        public IEnumerable<StepBase> Steps { get; set; }


        public abstract StepBase[] GetAllSteps();

    }
}
