using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_HasParameters
    {
        public static CSharpQuery<IMethodSymbol> HasParameters<T1>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(query.Unit.GetMetaName<T1>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(query.Unit.GetMetaName<T1>(), query.Unit.GetMetaName<T2>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.HasParameters(
                query.Unit.GetMetaName<T1>(),
                query.Unit.GetMetaName<T2>(),
                query.Unit.GetMetaName<T3>(),
                query.Unit.GetMetaName<T4>(),
                query.Unit.GetMetaName<T5>(),
                query.Unit.GetMetaName<T6>());
        }

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6, T7>(this CSharpQuery<IMethodSymbol> query)
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

        public static CSharpQuery<IMethodSymbol> HasParameters<T1, T2, T3, T4, T5, T6, T7, T8>(
            this CSharpQuery<IMethodSymbol> query)
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

        public static CSharpQuery<IMethodSymbol> HasParameters(this CSharpQuery<IMethodSymbol> query, params string[] parameters)
        {
            return HasParameters(query, parameters.ToImmutableArray());
        }

        private static CSharpQuery<IMethodSymbol> HasParameters(CSharpQuery<IMethodSymbol> query, ImmutableArray<string> parameters)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<IMethodSymbol>(query.Node);
                foreach (var method in result)
                {
                    if (parameters.Length != method.Parameters.Length)
                    {
                        context.Report(
                            message: Resources.ERR_WrongParemeterCount,
                            arguments: new object[] { method, parameters.Length },
                            symbol: method);
                        continue;
                    }

                    for (var i = 0; i < method.Parameters.Length; i++)
                    {
                        var expectedParameter = context.Locate<ITypeSymbol>(NameNode.Parse(parameters[i]));
                        if (!method.Parameters[i].Type.Equals(expectedParameter))
                        {
                            context.Report(
                                message: Resources.ERR_WrongParameterType,
                                arguments: new object[] { expectedParameter },
                                symbol: method.Parameters[i]);
                        }
                    }
                }
            });
            return query;
        }
    }
}