using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public interface ICSharpProject : IValidationUnit
    {
        ICSharpEvent GetEvent(string name);

        ICSharpField GetField(string name);

        ICSharpMethod GetMethod(string name);

        ICSharpProject GetProperty(string name);

        ICSharpType GetType(string name);

        void Validate(DynamicValidationAction action);
    }
}