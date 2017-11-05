using DotvvmAcademy.Validation.CSharp.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpAccessor : DefaultCSharpObject, ICSharpAccessor
    {
        public CSharpAccessModifier AccessModifier { get; set; }
    }
}