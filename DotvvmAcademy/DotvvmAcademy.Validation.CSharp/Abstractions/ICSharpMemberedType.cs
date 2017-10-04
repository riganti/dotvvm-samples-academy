namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# type that can contain members i.e. a class, a struct or an interface.
    /// </summary>
    public interface ICSharpMemberedType : ICSharpAllowsAccessModifier, ICSharpAllowsGenericParameters
    {
        ICSharpMethod Method();

        ICSharpProperty Property();
    }
}