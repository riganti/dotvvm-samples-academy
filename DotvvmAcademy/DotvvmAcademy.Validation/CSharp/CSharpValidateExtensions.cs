using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpValidateExtensions
    {
        public static CSharpTypeDescriptor Descriptor(this CSharpValidate validate, Type type, params CSharpTypeDescriptor[] genericParameters)
            => validate.Descriptor(type.FullName, genericParameters);

        public static CSharpTypeDescriptor Descriptor<TType>(this CSharpValidate validate, params CSharpTypeDescriptor[] genericParameters)
            => validate.Descriptor(typeof(TType), genericParameters);
    }
}