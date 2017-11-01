namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# field.
    /// </summary>
    public interface ICSharpField : ICSharpAllowsAccessModifier, ICSharpAllowsVolatileModifier, ICSharpAllowsConstModifier, ICSharpAllowsStaticModifier
    {
        CSharpTypeDescriptor Type { get; set; }
    }
}