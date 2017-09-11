using System;

namespace DotvvmAcademy.Validation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidatorAttribute : Attribute
    {
        public ValidatorAttribute(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public int Timeout { get; set; }

        public string CompiledAssemblyName { get; set; }
    }
}