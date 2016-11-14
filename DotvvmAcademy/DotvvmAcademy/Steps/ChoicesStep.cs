using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.Steps
{
    public class ChoicesStep : StepBase
    {
        public ChoicesStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        [Bind(Direction.ServerToClient)]
        public ChoiceStepOption[] Options { get; set; }
        
        [Bind(Direction.ServerToClient)]
        public int CorrectId { get; set; }

        public int SelectedId { get; set; } = -1;



        protected override IEnumerable<string> GetErrors()
        {
            if (SelectedId != CorrectId)
            {
                yield return "The answer is not correct.";
            }
        }
    }
}
