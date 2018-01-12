namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that allows the readonly modifier.
    /// </summary>
    public interface ICSharpAllowsReadonlyModifier : ICSharpObject
    {
        bool IsReadonly { get; set; }
    }
}