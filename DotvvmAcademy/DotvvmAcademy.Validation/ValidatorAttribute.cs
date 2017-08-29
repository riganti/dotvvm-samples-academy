using System;

namespace DotvvmAcademy.Validation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidatorAttribute : Attribute
    {
        public ValidatorAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}