using System;

namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    public class PreservePropertyAttribute : Attribute
    {
        public bool RemoveProperty { get; set; } = false;
    }
}