namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that allows the virtual modifier.
    /// </summary>
    public interface ICSharpAllowsVirtualModifier : ICSharpAllowsOverrideModifier, ICSharpObject
    {
        bool IsVirtual { get; set; }
    }
}