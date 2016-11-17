using System.Collections.Generic;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.StepsBases;

namespace DotvvmAcademy.Steps
{
    public class InfoStep : StepBase
    {
        public InfoStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        protected override IEnumerable<string> GetErrors()
        {
            //todo... 
            yield break;
        }
    }
}