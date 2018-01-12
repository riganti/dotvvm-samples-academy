namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that allows the sealed modifier.
    /// </summary>
    public interface ICSharpAllowsSealedModifier : ICSharpObject
    {
        bool IsSealed { get; set; }
    }
}