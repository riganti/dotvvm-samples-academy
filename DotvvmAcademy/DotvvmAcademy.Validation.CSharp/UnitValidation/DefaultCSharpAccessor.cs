using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpAccessor : DefaultCSharpObject, ICSharpAccessor
    {
        public DefaultCSharpAccessor(ICSharpNameStack nameStack) : base(nameStack)
        {
        }

        public CSharpAccessModifier AccessModifier { get; set; }
    }
}