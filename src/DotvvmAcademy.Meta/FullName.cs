using Microsoft.CodeAnalysis;
using System;
using System.Reflection;
using System.Text;

namespace DotvvmAcademy.Meta
{
    public static class FullName
    {
        public static string GetFullName(Type type, Qualification qualification = Qualification.None)
        {
            switch(qualification)
            {
                case Qualification.Full:
                    return type.AssemblyQualifiedName;
                case Qualification.Assembly:
                    return $"{type.FullName}, {type.Assembly.GetName().Name}";
                default:
                    return type.FullName;
            }
        }

        public static string GetFullName(INamedTypeSymbol type, Qualification qualification = Qualification.None)
        {
            var sb = new StringBuilder();
            if (type.ConstructedFrom != null)
            {
                sb.Append(BuildConstructedFullName(type, qualification));
            }
            else if(type.)
        }

        private static string BuildTopLevelFullName(INamedTypeSymbol type)
        {
            return $"{type.ContainingNamespace.ToDisplayString()}.{type.MetadataName}";
        }

        private static string BuildNestedFullName(INamedTypeSymbol type, Qualification qualification)
        {
            return $"{GetFullName(type.ContainingType, qualification)}+{type.MetadataName}";
        }

        private static string BuildConstructedFullName(INamedTypeSymbol type, Qualification qualification)
        {
            var sb = new StringBuilder();
            sb.Append(GetFullName(type.ConstructedFrom, qualification));
            sb.Append('[');
            for (int i = 0; i < type.TypeArguments.Length; i++)
            {
                sb.Append('[');
                sb.Append(GetFullName((INamedTypeSymbol)type.TypeArguments[i], qualification));
                sb.Append(']');
                if(i < type.TypeArguments.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
