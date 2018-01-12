namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that can be static.
    /// </summary>
    public interface ICSharpAllowsStaticModifier : ICSharpObject
    {
        bool IsStatic { get; set; }
    }
}