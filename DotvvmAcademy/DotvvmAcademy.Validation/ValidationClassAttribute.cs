using System;

namespace DotvvmAcademy.Validation
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationClassAttribute : Attribute
    {
        public ValidationClassAttribute()
        {
        }

        public string CompiledAssemblyName { get; set; }
    }
}