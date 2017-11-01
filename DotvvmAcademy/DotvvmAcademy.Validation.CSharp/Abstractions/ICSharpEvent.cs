namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# event field or property.
    /// </summary>
    public interface ICSharpEvent : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier
    {
        CSharpTypeDescriptor Type { get; set; }

        bool IsProperty { get; set; }
    }
}