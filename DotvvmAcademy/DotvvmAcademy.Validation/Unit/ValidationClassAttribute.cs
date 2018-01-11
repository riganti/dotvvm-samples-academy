using System;

namespace DotvvmAcademy.Validation.Unit
{
    /// <summary>
    /// Specifies that a class is to be considered a Validation Class and thus contain Validation Methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationClassAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="ValidationClassAttribute"/>.
        /// </summary>
        public ValidationClassAttribute()
        {
        }
    }
}