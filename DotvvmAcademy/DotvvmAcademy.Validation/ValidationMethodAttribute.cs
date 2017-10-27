using System;

namespace DotvvmAcademy.Validation
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationMethodAttribute : Attribute
    {
        public ValidationMethodAttribute()
        {
        }
        
        public string Name { get; set; }
    }
}