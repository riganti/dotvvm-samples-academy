using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
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