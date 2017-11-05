namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# field.
    /// </summary>
    public interface ICSharpField : ICSharpAllowsAccessModifier, ICSharpAllowsVolatileModifier, ICSharpAllowsConstModifier, ICSharpAllowsStaticModifier, ICSharpObject
    {
        CSharpTypeDescriptor Type { get; set; }
    }
}