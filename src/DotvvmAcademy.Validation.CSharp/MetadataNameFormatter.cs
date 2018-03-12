using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class MetadataNameFormatter : IMetadataNameFormatter
    {
        public const string ArityPrefix = "`";
        public const string ArrayEnd = "]";
        public const string ArrayRankSeparator = ",";
        public const string ArrayStart = "[";
        public const string MemberAccess = "::";
        public const string NamespaceAccess = ".";
        public const string NestedTypeAccess = "/";
        public const string ParameterListEnd = ")";
        public const string ParameterListSeparator = ",";
        public const string ParameterListStart = "(";
        public const string PointerSuffix = "*";
        public const string ReturnTypeSeparator = " ";
        public const string TypeArgumentListEnd = ">";
        public const string TypeArgumentListSeparator = ",";
        public const string TypeArgumentListStart = "<";
        public const string TypeParameterAccess = "%";

        public string Format(MetadataName metadataName)
        {
            switch (metadataName.Kind)
            {
                case MetadataNameKind.Type:
                    return FormatType(metadataName);

                case MetadataNameKind.Member:
                    return FormatMember(metadataName);

                default:
                    throw new InvalidOperationException($"{nameof(MetadataNameFormatter)} can't format {nameof(MetadataName)} with Kind '{metadataName.Kind}'.");
            }
        }

        private string FormatArity(int arity)
        {
            if (arity > 0)
            {
                return $"{ArityPrefix}{arity}";
            }
            return "";
        }

        private string FormatArrayType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(ArrayStart);
            for (int i = 1; i < metadataName.Rank; i++)
            {
                sb.Append(ArrayRankSeparator);
            }
            sb.Append(ArrayEnd);
            return sb.ToString();
        }

        private string FormatConstructedMethod(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(FormatField(metadataName.Owner));
            sb.Append(FormatTypeArguments(metadataName.TypeArguments));
            sb.Append(FormatParameters(metadataName.Owner.Parameters));
            return sb.ToString();
        }

        private string FormatConstructedType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(FormatType(metadataName.Owner, false));
            sb.Append(FormatTypeArguments(metadataName.TypeArguments));
            return sb.ToString();
        }

        private string FormatField(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.ReturnType));
            sb.Append(ReturnTypeSeparator);
            sb.Append(Format(metadataName.Owner));
            sb.Append(MemberAccess);
            sb.Append(metadataName.Name);
            return sb.ToString();
        }

        private string FormatMember(MetadataName metadataName)
        {
            switch (metadataName.Kind)
            {
                case MetadataNameKind.Method | MetadataNameKind.ConstructedMethod:
                    return FormatConstructedMethod(metadataName);

                case MetadataNameKind.Method:
                    return FormatMethod(metadataName);

                default:
                    return FormatField(metadataName);
            }
        }

        private string FormatMethod(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(FormatField(metadataName));
            sb.Append(FormatArity(metadataName.Arity));
            sb.Append(FormatParameters(metadataName.Parameters));
            return sb.ToString();
        }

        private string FormatNestedType(MetadataName metadataName, bool includeArity)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(metadataName.Name);
            if (includeArity)
            {
                sb.Append(FormatArity(metadataName.Arity));
            }

            return sb.ToString();
        }

        private string FormatParameters(IList<MetadataName> parameters)
        {
            var sb = new StringBuilder();
            sb.Append(ParameterListStart);
            if (parameters.Count > 0)
            {
                sb.Append(Format(parameters[0]));
                for (int i = 1; i < parameters.Count; i++)
                {
                    sb.Append(ParameterListSeparator);
                    sb.Append(Format(parameters[i]));
                }
            }
            sb.Append(ParameterListEnd);
            return sb.ToString();
        }

        private string FormatPointerType(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(PointerSuffix);
            return sb.ToString();
        }

        private string FormatTopLevelType(MetadataName metadataName, bool includeArity = true)
        {
            var sb = new StringBuilder();
            sb.Append(metadataName.Namespace);
            if (metadataName.Namespace.Length > 0)
            {
                sb.Append(NamespaceAccess);
            }

            sb.Append(metadataName.Name);
            if (includeArity)
            {
                sb.Append(FormatArity(metadataName.Arity));
            }

            return sb.ToString();
        }

        private string FormatType(MetadataName metadataName, bool includeArity = true)
        {
            switch (metadataName.Kind)
            {
                case MetadataNameKind.ConstructedType:
                    return FormatConstructedType(metadataName);

                case MetadataNameKind.NestedType:
                    return FormatNestedType(metadataName, includeArity);

                case MetadataNameKind.ArrayType:
                    return FormatArrayType(metadataName);

                case MetadataNameKind.PointerType:
                    return FormatPointerType(metadataName);

                case MetadataNameKind.TypeParameter:
                    return FormatTypeParameter(metadataName);

                default:
                    return FormatTopLevelType(metadataName, includeArity);
            }
        }

        private string FormatTypeArguments(IList<MetadataName> typeArguments)
        {
            if (typeArguments.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.Append(TypeArgumentListStart);
            sb.Append(Format(typeArguments[0]));
            for (int i = 1; i < typeArguments.Count; i++)
            {
                sb.Append(TypeArgumentListSeparator);
                sb.Append(Format(typeArguments[i]));
            }
            sb.Append(TypeArgumentListEnd);
            return sb.ToString();
        }

        private string FormatTypeParameter(MetadataName metadataName)
        {
            var sb = new StringBuilder();
            sb.Append(Format(metadataName.Owner));
            sb.Append(TypeParameterAccess);
            sb.Append(metadataName.Name);
            return sb.ToString();
        }
    }
}