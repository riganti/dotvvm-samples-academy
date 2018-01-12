namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# member or type that can have an access modifier.
    /// </summary>
    public interface ICSharpAllowsAccessModifier : ICSharpObject
    {
        CSharpAccessModifier AccessModifier { get; set; }
    }
}