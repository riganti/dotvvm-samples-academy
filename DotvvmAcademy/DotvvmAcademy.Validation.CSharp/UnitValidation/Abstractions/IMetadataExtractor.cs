namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public interface IMetadataExtractor
    {
        void ExtractMetadata(ICSharpObjectFactory factory, CSharpValidationRequest request);
    }
}