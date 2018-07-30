using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public interface IMetadataNameFactory
    {
        MetadataName CreateArrayTypeName(MetadataName owner, int rank);

        MetadataName CreateConstructedMethodName(MetadataName owner, ImmutableArray<MetadataName> typeArguments);

        MetadataName CreateConstructedTypeName(MetadataName owner, ImmutableArray<MetadataName> typeArguments);

        MetadataName CreateFieldName(MetadataName owner, string name, MetadataName returnType);

        MetadataName CreateMethodName(
            MetadataName owner,
            string name,
            MetadataName returnType = null,
            int arity = 0,
            ImmutableArray<MetadataName> parameters = default);

        MetadataName CreateNestedTypeName(MetadataName owner, string name, int arity = 0);

        MetadataName CreatePointerType(MetadataName owner);

        MetadataName CreateTypeName(string @namespace, string name, int arity = 0);

        MetadataName CreateTypeParameterName(MetadataName owner, string name);
    }
}