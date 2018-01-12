namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that can be marked as abstract.
    /// </summary>
    public interface ICSharpAllowsAbstractModifier : ICSharpObject
    {
        bool IsAbstract { get; set; }
    }
}