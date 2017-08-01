using System;

namespace DotvvmAcademy.Steps.Validation.ValidatorProvision
{
    /// <summary>
    /// Attribute for matching xml data and current validator.
    /// </summary>
    public class StepValidationAttribute : Attribute
    {
        public string ValidatorKey { get; set; }
    }
}