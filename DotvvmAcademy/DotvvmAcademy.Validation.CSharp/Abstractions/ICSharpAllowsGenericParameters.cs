namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# member or type that allows generic parameters.
    /// </summary>
    public interface ICSharpAllowsGenericParameters
    {
        ICSharpGenericParameter GenericParameter(string name);
    }
}