using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryExtensions
    {
        public static IQuery<TResult> Allow<TResult>(this IQuery<TResult> query)
            where TResult : ISymbol
        {
            query.SetConstraint(nameof(Allow), context =>
            {
                var storage = context.Provider.GetRequiredService<AllowedSymbolStorage>();
                foreach (var symbol in context.Result)
                {
                    storage.Allow(symbol);
                }
            });
            return query;
        }

        public static IQuery<TResult> HasAccessibility<TResult>(
            this IQuery<TResult> query,
            CSharpAccessibility accessibility)
            where TResult : ISymbol
        {
            query.SetConstraint(nameof(HasAccessibility), context =>
            {
                foreach (var symbol in context.Result)
                {
                    if (!accessibility.HasFlag(symbol.DeclaredAccessibility.ToCSharpAccessibility()))
                    {
                        context.Report(
                            message: $"Symbol '{symbol}' has to declare accessibility '{accessibility}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }

        public static IQuery<ITypeSymbol> IsTypeKind(
                    this IQuery<ITypeSymbol> query,
            CSharpTypeKind typeKind)
        {
            query.SetConstraint(nameof(IsTypeKind), context =>
            {
                foreach (var symbol in context.Result)
                {
                    if (!typeKind.HasFlag(symbol.TypeKind.ToCSharpTypeKind()))
                    {
                        context.Report(
                            message: $"Type '{symbol}' has to be a '{typeKind}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }
    }
}