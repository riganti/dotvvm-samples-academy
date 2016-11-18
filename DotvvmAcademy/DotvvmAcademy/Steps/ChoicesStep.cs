using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps
{
    public class ChoicesStep : StepBase
    {
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