using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Steps.StepsBases;
using System.Collections.Generic;

namespace DotvvmAcademy.Steps
{
    public class ChoicesStep : ValidableStepBase
    {
        [Bind(Direction.ServerToClient)]
        public int CorrectId { get; set; }

        [Bind(Direction.ServerToClient)]
        public ChoiceStepOption[] Options { get; set; }

        public int SelectedId { get; set; } = -1;

        protected override IEnumerable<string> GetValidationErrors()
        {
            if (SelectedId != CorrectId)
            {
                yield return "The answer is not correct.";
            }
        }
    }
}