namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that can have an access modifier.
    /// </summary>
    public interface ICSharpAllowsAccessModifier
    {
        CSharpAccessModifier AccessModifier { get; set; }
    }
}