using DotvvmAcademy.Steps.StepsBases.Interfaces;
using System.Collections.Generic;

namespace DotvvmAcademy.Steps.StepsBases
{
    public abstract class ValidableStepBase : IStep
    {
        public string Description { get; set; }

        public int StepIndex { get; set; }

        public string Title { get; set; }

        public string Validate()
        {
            return string.Join(" ", GetValidationErrors());
        }

        protected abstract IEnumerable<string> GetValidationErrors();
    }
}