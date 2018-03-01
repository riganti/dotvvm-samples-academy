namespace DotvvmAcademy.Validation.CSharp
{
    public interface IMetadataNameProvider<TMetadataSource>
    {
        MetadataName GetName(TMetadataSource source);
    }
}