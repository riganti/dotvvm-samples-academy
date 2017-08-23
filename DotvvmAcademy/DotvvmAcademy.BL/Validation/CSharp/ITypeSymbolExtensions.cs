using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public static class ITypeSymbolExtensions
    {
        public static bool EqualsType(this ITypeSymbol symbol, Type type)
        {
            var sameName = symbol.Name == type.Name;
            var sameNamespace = symbol.ContainingNamespace.Name == type.Namespace;
            var sameAssembly = symbol.ContainingAssembly.ToDisplayString() == type.Assembly.FullName;
            return sameName && sameNamespace && sameAssembly;
        }

        public static bool EqualsTypeFullName(this ITypeSymbol symbol, string typeFullName)
        {
            return $"{symbol.ContainingNamespace.Name}.{symbol.Name}" == typeFullName;
        }
    }
}
