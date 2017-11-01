namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpConversionOperator : ICSharpAllowsAccessModifier
    {
        CSharpConversionModifier ConversionModifier { get; set; }
    }
}