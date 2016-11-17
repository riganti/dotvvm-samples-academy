using System;

namespace DotvvmAcademy.Steps.Validation
{
    public class StepValidationAttribute : Attribute
    {
        public string ValidationKey { get; set; }
    }
}