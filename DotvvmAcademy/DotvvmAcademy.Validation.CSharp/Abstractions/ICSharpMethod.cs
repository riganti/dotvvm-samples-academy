namespace DotvvmAcademy.Validation.CSharp.Abstractions
{

    /// <summary>
    /// A C# method.
    /// </summary>
    public interface ICSharpMethod : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsAsyncModifier, ICSharpAllowsGenericParameters, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier
    {
        void ReturnType(ICSharpTypeDescriptor type);
    }
}