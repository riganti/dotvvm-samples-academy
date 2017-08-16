using System;

namespace DotvvmAcademy.BL.Validation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class ValidatorAttribute : Attribute
    {
        private readonly string id;

        public ValidatorAttribute(string id)
        {
            this.id = id;
        }

        public string Id
        {
            get { return id; }
        }
    }
}