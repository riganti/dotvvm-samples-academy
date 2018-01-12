namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# member or type that can be marked as abstract.
    /// </summary>
    public interface ICSharpAllowsAbstractModifier : ICSharpObject
    {
        bool IsAbstract { get; set; }
    }
}