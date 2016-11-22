using System;

namespace DotvvmAcademy.Steps.Validation.ValidatorProvision
{
    public class StepValidationAttribute : Attribute
    {
        public string ValidatorKey { get; set; }
    }
}