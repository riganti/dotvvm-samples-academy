using DotvvmAcademy.Validation.CSharp.Unit;
using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A lightweight representation of a C# type (i.e. class, struct, interface or enum) from the validated code or a referenced assembly.
    /// </summary>
    public class CSharpTypeDescriptor
    {
        public CSharpTypeDescriptor(string fullName)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }

        public string FullName { get; set; }

        public static implicit operator CSharpTypeDescriptor(Type type)
        {
           return new CSharpTypeDescriptor(type.FullName);
        }
    }
}