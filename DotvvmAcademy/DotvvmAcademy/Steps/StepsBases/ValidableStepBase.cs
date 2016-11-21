using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Steps.StepsBases
{
    public abstract class ValidableStepBase : IStep
    {
        public string ErrorMessage => string.Join(" ", GetErrors());
        public int StepIndex { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        protected abstract IEnumerable<string> GetErrors();
    }
}