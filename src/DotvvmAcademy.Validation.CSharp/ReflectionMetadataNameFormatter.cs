using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class ReflectionMetadataNameFormatter : IMetadataNameFormatter
    {
        public const string TypeArgumentListStart = "[";
        public const string TypeArgumentListEnd = "]";
        public const string MemberAccess = ".";
        public const string NestedTypeAccess = "+";
        public const string ArityPrefix = "`";
        public const string NamespaceAccess = ".";
        public const string TypeArgumentListSeparator = ",";


        public string Format(MetadataName metadataName)
        {
            switch (metadataName.Kind)
            {
                case MetadataNameKind.Type | MetadataNameKind.ConstructedType:
                    return FormatConstructedType(metadataName);
                case MetadataNameKind.Type | MetadataNameKind.NestedType:
                    return FormatNestedType(metadataName);
                case MetadataNameKind.Type:
                    return FormatTopLevelType(metadataName);
                default:
                    return metadataName.Name;
            }
        }

        private string FormatConstructedType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(FormatTypeArguments(metadataName.TypeArguments));
            return sb.ToString();
        }

        private string FormatTopLevelType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(metadataName.Namespace);
            sb.Append(NamespaceAccess);
            sb.Append(metadataName.Name);
            sb.Append(FormatArity(metadataName.Arity));
            return sb.ToString();
        }

        private string FormatNestedType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(NestedTypeAccess);
            sb.Append(metadataName.Name);
            sb.Append(FormatArity(metadataName.Arity));
            return sb.ToString();
        }

        private string FormatArity(int arity)
        {
            if (arity > 0)
            {
                return $"{ArityPrefix}{arity}";
            }

            return "";
        }

        private string FormatTypeArguments(IList<MetadataName> typeArguments)
        {
            var sb = new StringBuilder();
            sb.Append(TypeArgumentListStart);
            sb.Append(Format(typeArguments[0]));
            for (int i = 1; i < typeArguments.Count; i++)
            {
                sb.Append(TypeArgumentListSeparator);
                sb.Append(typeArguments[i]);
            }
            sb.Append(TypeArgumentListEnd);
            return sb.ToString();
        }
    }
}
