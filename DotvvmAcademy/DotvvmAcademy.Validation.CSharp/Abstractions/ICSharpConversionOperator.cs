namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpConversionOperator : ICSharpAllowsAccessModifier, ICSharpObject
    {
        CSharpConversionModifier ConversionModifier { get; set; }
    }
}