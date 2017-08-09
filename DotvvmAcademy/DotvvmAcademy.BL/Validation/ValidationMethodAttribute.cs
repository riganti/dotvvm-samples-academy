using System;

namespace DotvvmAcademy.BL.Validation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class ValidationMethodAttribute : Attribute
    {
        readonly string id;

        public ValidationMethodAttribute(string id)
        {
            this.id = id;
        }

        public string PositionalString
        {
            get { return id; }
        }
    }
}
