namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the sealed modifier.
    /// </summary>
    public interface ICSharpAllowsSealedModifier : ICSharpObject
    {
        bool IsSealed { get; set; }
    }
}