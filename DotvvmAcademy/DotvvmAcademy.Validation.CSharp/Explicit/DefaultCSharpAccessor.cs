using DotvvmAcademy.Validation.CSharp.Abstractions;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpAccessor : DefaultCSharpObject, ICSharpAccessor
    {
        public CSharpAccessModifier AccessModifier { get; set; }
    }
}