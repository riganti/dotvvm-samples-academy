namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# document.
    /// </summary>
    public interface ICSharpDocument
    {
        ICSharpNamespace GlobalNamespace();

        ICSharpNamespace Namespace(string name);
    }
}