namespace DotvvmAcademy.Validation.CSharp.Abstractions
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