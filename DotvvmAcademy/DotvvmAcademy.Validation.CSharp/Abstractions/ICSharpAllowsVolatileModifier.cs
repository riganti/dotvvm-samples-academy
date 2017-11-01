namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the volatile modifier.
    /// </summary>
    public interface ICSharpAllowsVolatileModifier
    {
        bool IsVolatile { get; set; }
    }
}