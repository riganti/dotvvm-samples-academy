namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# event field or property.
    /// </summary>
    public interface ICSharpEvent : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier
    {
        void Type(ICSharpTypeDescriptor type);

        void AddAccessor();

        void RemoveAccessor();
    }
}