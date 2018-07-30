using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions
    {
        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            query.AddConstraint(context =>
            {
                var storage = context.Provider.GetRequiredService<AllowedSymbolStorage>();
                foreach (var symbol in context.Result)
                {
                    storage.Allow(symbol);
                }
            });
            return query;
        }

        public static CSharpQuery<TResult> CountEquals<TResult>(this CSharpQuery<TResult> query, int count)
            where TResult : ISymbol
        {
            query.AddConstraint(context =>
            {
                if (context.Result.Length != count)
                {
                    context.Report(
                        message: $"Found '{context.Result.Length}' of '{context.Name}' " +
                            $"but expected to find '{count}'.");
                }
            });
            return query;
        }

        public static CSharpQuery<ITypeSymbol> IsTypeKind(
            this CSharpQuery<ITypeSymbol> query, 
            CSharpTypeKind typeKind)
        {
            query.AddConstraint(context =>
            {
                foreach (var symbol in context.Result)
                {
                    if (!typeKind.HasFlag(symbol.TypeKind.ToCSharpTypeKind()))
                    {
                        context.Report($"Type '{symbol}' has to be a '{typeKind}'.", symbol);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<TResult> HasAccessibility<TResult>(
            this CSharpQuery<TResult> query,
            CSharpAccessibility accessibility)
            where TResult : ISymbol
        {
            query.AddConstraint(context =>
            {
                foreach(var symbol in context.Result)
                {
                    if(!accessibility.HasFlag(symbol.DeclaredAccessibility.ToCSharpAccessibility()))
                    {
                        context.Report($"Symbol '{symbol}' has to declare accessibility '{accessibility}'.", symbol);
                    }
                }
            });
            return query;
        }
    }
}