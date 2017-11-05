namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface IMetadataExtractor
    {
        void ExtractMetadata(ICSharpFactory factory, CSharpValidationRequest request);
    }
}