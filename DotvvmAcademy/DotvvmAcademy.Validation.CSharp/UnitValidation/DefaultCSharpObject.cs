using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpObject : ICSharpObject
    {
        public DefaultCSharpObject(ICSharpNameStack nameStack)
        {
            FullName = nameStack.PopName();
        }

        public string FullName { get; }
    }
}