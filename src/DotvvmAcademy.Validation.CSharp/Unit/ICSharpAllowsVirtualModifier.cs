namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the virtual modifier.
    /// </summary>
    public interface ICSharpAllowsVirtualModifier : ICSharpAllowsOverrideModifier, ICSharpObject
    {
        bool IsVirtual { get; set; }
    }
}