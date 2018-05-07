using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public interface ICSharpType : ICSharpAllowsAccessModifier, ICSharpAllowsStaticModifier
    {
        ICSharpEvent GetEvent(string name);

        ICSharpField GetField(string name);

        ICSharpMethod GetMethod(string name);

        ICSharpProject GetProperty(string name);

        ICSharpType GetType(string name);
    }
}