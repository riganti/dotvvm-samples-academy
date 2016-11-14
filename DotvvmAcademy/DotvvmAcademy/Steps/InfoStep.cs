using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.Steps
{
    public class InfoStep : StepBase
    {
        public InfoStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        protected override IEnumerable<string> GetErrors()
        {
            yield break;
        }
    }
}
