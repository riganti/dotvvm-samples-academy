using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class MetaConverterExtensions
    {
        public static MemberInfo ToReflectionSingle(this MetaConverter converter, NameNode node)
        {
            return GetSingle(converter.ToReflection(node), node);
        }

        public static MemberInfo ToReflectionSingle(this MetaConverter converter, ISymbol symbol)
        {
            return GetSingle(converter.ToReflection(symbol), symbol);
        }

        public static ISymbol ToRoslynSingle(this MetaConverter converter, NameNode node)
        {
            return GetSingle(converter.ToRoslyn(node), node);
        }

        public static ISymbol ToRoslynSingle(this MetaConverter converter, MemberInfo info)
        {
            return GetSingle(converter.ToRoslyn(info), info);
        }

        private static TOutput GetSingle<TInput, TOutput>(IEnumerable<TOutput> outputs, TInput input)
        {
            var result = outputs.ToArray();
            if (result.Length == 0)
            {
                throw new InvalidOperationException($"{typeof(TInput).Name} \"{input}\" could not be converted " +
                    $"to a {typeof(TOutput).Name}.");
            }
            else if (result.Length > 1)
            {
                var conversions = string.Join(",\n", outputs);
                throw new InvalidOperationException($"{typeof(TInput).Name} \"{input}\" has multiple " +
                    $"{typeof(TOutput).Name} conversions:\n{conversions}.");
            }
            else
            {
                return result[0];
            }
        }
    }
}