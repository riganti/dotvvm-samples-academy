namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface ICSharpConversionOperator : ICSharpAllowsAccessModifier, ICSharpObject
    {
        CSharpConversionModifier ConversionModifier { get; set; }
    }
}