using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps.StepsBases
{
    public abstract class StepBase : IStep
    {
        public string ErrorMessage => string.Join(" ", GetErrors());
        public int StepIndex { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Title { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Description { get; set; }

        protected abstract IEnumerable<string> GetErrors();
    }
}