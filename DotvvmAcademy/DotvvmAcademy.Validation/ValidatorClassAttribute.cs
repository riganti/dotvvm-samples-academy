using System;

namespace DotvvmAcademy.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ValidatorClassAttribute : Attribute
    {
        public ValidatorClassAttribute(string compiledAssemblyName)
        {
            CompiledAssemblyName = compiledAssemblyName;
        }

        public string CompiledAssemblyName { get; }
    }
}