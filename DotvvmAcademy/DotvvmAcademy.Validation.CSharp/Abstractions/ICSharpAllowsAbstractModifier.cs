namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that can be marked as abstract.
    /// </summary>
    public interface ICSharpAllowsAbstractModifier : ICSharpAllowsOverrideModifier, ICSharpObject
    {
        bool IsAbstract { get; set; }
    }
}