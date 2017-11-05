namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# property.
    /// </summary>
    public interface ICSharpProperty : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject
    {
        ICSharpAccessor GetGetter();

        ICSharpAccessor GetSetter();

        CSharpTypeDescriptor Type { get; set; }

    }
}