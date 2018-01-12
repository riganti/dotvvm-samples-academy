using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpAccessor : DefaultCSharpObject, ICSharpAccessor
    {
        public DefaultCSharpAccessor(ICSharpNameStack nameStack) : base(nameStack)
        {
        }

        public CSharpAccessModifier AccessModifier { get; set; }
    }
}