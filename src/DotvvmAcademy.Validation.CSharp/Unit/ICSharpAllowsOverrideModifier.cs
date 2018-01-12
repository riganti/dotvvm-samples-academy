namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that can override its base.
    /// </summary>
    public interface ICSharpAllowsOverrideModifier : ICSharpObject
    {
        bool IsOverriding { get; set; }
    }
}