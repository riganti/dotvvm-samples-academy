using System;

namespace DotvvmAcademy.Validation.Unit
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationClassAttribute : Attribute
    {
        public ValidationClassAttribute()
        {
        }
    }
}