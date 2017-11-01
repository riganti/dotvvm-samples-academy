namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the readonly modifier.
    /// </summary>
    public interface ICSharpAllowsReadonlyModifier
    {
        bool IsReadonly { get; set; }
    }
}