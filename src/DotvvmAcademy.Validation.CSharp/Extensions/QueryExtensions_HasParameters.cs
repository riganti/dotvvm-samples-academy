using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryExtensions_HasParameters
    {
        public static IQuery<IMethodSymbol> HasParameters<T1>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(query.Unit.GetMetaName<T1>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(query.Unit.GetMetaName<T1>(), query.Unit.GetMetaName<T2>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>(),
                query.Unit.GetMetaName<T6>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6, T7>(this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>(),
                query.Unit.GetMetaName<T6>(),
                query.Unit.GetMetaName<T7>());
        }

        public static IQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>(),
                query.Unit.GetMetaName<T6>(),
                query.Unit.GetMetaName<T7>(),
                query.Unit.GetMetaName<T8>());
        }

        public static IQuery<IMethodSymbol> HasParameters(this IQuery<IMethodSymbol> query, params string[] parameters)
        {
            return HasParameters(query, parameters.ToImmutableArray());
        }

        private static IQuery<IMethodSymbol> HasParameters(
            IQuery<IMethodSymbol> query,
            ImmutableArray<string> parameters)
        {
            query.SetConstraint(nameof(HasParameters), context =>
            {
                var locator = context.Provider.GetRequiredService<SymbolLocator>();
                foreach (var method in context.Result)
                {
                    if (parameters.Length != method.Parameters.Length)
                    {
                        context.Report(
                            message: $"Method must have '{parameters.Length}' parameters.",
                            symbol: method);
                        continue;
                    }

                    for (int i = 0; i < method.Parameters.Length; i++)
                    {
                        var expectedParameter = locator.LocateSingle(parameters[i]);
                        if (!method.Parameters[i].Equals(expectedParameter))
                        {
                            context.Report(
                                message: $"Parameters must be of type '{expectedParameter}'.",
                                symbol: method.Parameters[i]);
                        }
                    }
                }
            });
            return query;
        }
    }
}