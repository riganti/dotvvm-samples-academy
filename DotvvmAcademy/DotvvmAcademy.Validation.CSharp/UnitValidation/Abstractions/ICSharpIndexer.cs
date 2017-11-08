namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# indexer.
    /// </summary>
    public interface ICSharpIndexer : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        CSharpTypeDescriptor ReturnType { get; set; }

        ICSharpAccessor GetGetter();

        ICSharpAccessor GetSetter();
    }
}