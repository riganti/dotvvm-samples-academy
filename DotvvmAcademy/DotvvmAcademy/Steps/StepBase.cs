using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Steps
{
    public abstract class StepBase 
    {

        public int StepIndex { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Title { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Description { get; set; }
        
        public string ErrorMessage => string.Join(" ", GetErrors());

        protected abstract IEnumerable<string> GetErrors();
    }
}