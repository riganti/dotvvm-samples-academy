using System;

namespace DotvvmAcademy.Validation
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ValidationClassAttribute : Attribute
    {
        public ValidationClassAttribute()
        {
        }

        public string CompiledAssemblyName { get; set; }
    }
}