using System.Linq;

namespace Microsoft.CodeAnalysis
{
    internal static class NamedTypeSymbolExtensions
    {
        public static bool IsConstructedType(this INamedTypeSymbol symbol)
        {
            return symbol.IsGenericType && !symbol.TypeArguments.Any(t => t.TypeKind == TypeKind.TypeParameter);
        }
    }
}