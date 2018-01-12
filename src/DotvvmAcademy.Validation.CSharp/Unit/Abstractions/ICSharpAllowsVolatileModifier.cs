namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the volatile modifier.
    /// </summary>
    public interface ICSharpAllowsVolatileModifier : ICSharpObject
    {
        bool IsVolatile { get; set; }
    }
}