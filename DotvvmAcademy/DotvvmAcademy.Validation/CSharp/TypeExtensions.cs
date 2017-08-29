using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class TypeExtensions
    {
        public static CSharpTypeDescriptor GetDescriptor(this Type type, CSharpValidate validate, params CSharpTypeDescriptor[] genericParameters)
        {
            if (genericParameters.Any(p => !p.IsActive)) return CSharpTypeDescriptor.Inactive;
            return validate.Descriptor(type, genericParameters);
        }
    }
}