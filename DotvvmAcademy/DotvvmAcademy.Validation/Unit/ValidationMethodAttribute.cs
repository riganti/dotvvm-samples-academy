using System;

namespace DotvvmAcademy.Validation.Unit
{
    /// <summary>
    /// Specifies that a method is to be considered a Validation Method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationMethodAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="ValidationMethodAttribute"/>.
        /// </summary>
        public ValidationMethodAttribute()
        {
        }

        /// <summary>
        /// The name of the Validation Method.
        /// </summary>
        public string Name { get; set; }
    }
}