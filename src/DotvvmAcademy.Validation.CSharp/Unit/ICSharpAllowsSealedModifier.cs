namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the sealed modifier.
    /// </summary>
    public interface ICSharpAllowsSealedModifier : ICSharpObject
    {
        bool IsSealed { get; set; }
    }
}