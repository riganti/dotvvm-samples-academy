namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# namespace.
    /// </summary>
    public interface ICSharpNamespace
    {
        ICSharpClass Class(string name);

        ICSharpDelegate Delegate(string name);

        ICSharpEnum Enum(string name);

        ICSharpInterface Interface(string name);

        ICSharpStruct Struct(string name);
    }
}