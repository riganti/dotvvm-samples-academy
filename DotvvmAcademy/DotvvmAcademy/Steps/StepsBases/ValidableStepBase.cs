using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Steps.StepsBases
{
    public abstract class ValidableStepBase : IStep
    {
        public string Validate()
        {
            return string.Join(" ", GetValidationErrors());
        }

        public int StepIndex { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        protected abstract IEnumerable<string> GetValidationErrors();
    }
}