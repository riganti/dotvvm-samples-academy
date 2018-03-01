namespace DotvvmAcademy.Validation.CSharp
{
    public interface ILocator<TMetadataSource>
    {
        bool TryLocate(MetadataName name, out TMetadataSource metadataSource);
    }
}