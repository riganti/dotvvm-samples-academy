namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that can be static.
    /// </summary>
    public interface ICSharpAllowsStaticModifier
    {
        bool IsStatic { get; set; }
    }
}