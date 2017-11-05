namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# member or type that allows the readonly modifier.
    /// </summary>
    public interface ICSharpAllowsReadonlyModifier : ICSharpObject
    {
        bool IsReadonly { get; set; }
    }
}