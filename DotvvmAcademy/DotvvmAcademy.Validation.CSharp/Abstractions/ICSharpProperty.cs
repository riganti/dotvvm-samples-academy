namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# property.
    /// </summary>
    public interface ICSharpProperty : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier
    {
        ICSharpAccessor Getter();

        ICSharpAccessor Setter();

        void Type(CSharpTypeDescriptor type);

    }
}