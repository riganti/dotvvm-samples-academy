using Microsoft.CodeAnalysis;
using System;
using System.Text;

namespace DotvvmAcademy.Meta
{
    public static class FullNamer
    {
        public static string FromReflection(Type type, Qualification qualification = Qualification.None)
        {
            switch (qualification)
            {
                case Qualification.Full:
                    return type.AssemblyQualifiedName;

                case Qualification.Assembly:
                    return $"{type.FullName}, {type.Assembly.GetName().Name}";

                default:
                    return type.FullName;
            }
        }

        public static string FromRoslyn(ITypeSymbol type, Qualification qualification = Qualification.None)
        {
            var sb = new StringBuilder();
            // TODO: Handle constructed generic types, pointers, arrays, (and references?)
            if (type.ContainingType != null)
            {
                sb.Append(FromRoslyn(type.ContainingType));
                sb.Append('+');
                sb.Append(type.MetadataName);
            }
            else
            {
                // TODO: Fix case with the global namespace
                sb.Append(type.ContainingNamespace.ToDisplayString());
                sb.Append('.');
                sb.Append(type.Name);
            }
            
            switch(qualification)
            {
                case Qualification.Assembly:
                    sb.Append(", ");
                    sb.Append(type.ContainingAssembly.Identity.Name);
                    break;
                case Qualification.Full:
                    sb.Append(", ");
                    sb.Append(type.ContainingAssembly.Identity.ToString());
                    break;
            }
            return sb.ToString();
        }
    }
}