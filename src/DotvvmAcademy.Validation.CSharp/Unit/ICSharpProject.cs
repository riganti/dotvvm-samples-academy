using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# project.
    /// </summary>
    public interface ICSharpProject : IValidationUnit
    {
        ICSharpNamespace GetGlobalNamespace();

        ICSharpNamespace GetNamespace(string name);

        void Remove<TCSharpObject>(TCSharpObject csharpObject)
            where TCSharpObject : ICSharpObject;
    }
}