using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class MetadataNameFactoryExtensions
    {
        public static MetadataName CreateEventName(this IMetadataNameFactory factory, MetadataName owner, string name, MetadataName handlerType)
            => factory.CreateFieldName(owner, name, handlerType);

        public static MetadataName CreatePropertyName(this IMetadataNameFactory factory, MetadataName owner, string name, MetadataName type,
                    ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
            => factory.CreateMethodName(owner, name, type, parameters: parameters);
    }
}