namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# indexer.
    /// </summary>
    public interface ICSharpIndexer : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsVirtualModifier
    {
        CSharpTypeDescriptor ReturnType { get; set; }

        ICSharpAccessor GetGetter();

        ICSharpAccessor GetSetter();
    }
}