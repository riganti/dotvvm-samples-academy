using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class INamedTypeSymbolExtensions
    {
        public static CSharpTypeDescriptor GetDescriptor(this INamedTypeSymbol symbol, params CSharpTypeDescriptor[] genericParameters)
        {
            if (genericParameters.Any(p => !p.IsActive)) return CSharpTypeDescriptor.Inactive;

            if (genericParameters.Length > 0)
            {
                if (!symbol.IsGenericType)
                {
                    throw new NotSupportedException("The provided symbol is not generic therefore generic parameters are not supported.");
                }
                else
                {
                    symbol = symbol.Construct(genericParameters.Select(d => d.Symbol).ToArray());
                }
            }
            return new CSharpTypeDescriptor(symbol);
        }
    }
}