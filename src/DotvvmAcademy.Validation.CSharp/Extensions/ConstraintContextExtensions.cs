using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static ImmutableArray<ISymbol> Locate(this ConstraintContext context, NameNode name)
        {
            return context.Provider.GetRequiredService<IMetaConverter<NameNode, ISymbol>>()
                .Convert(name)
                .ToImmutableArray();
        }

        public static ImmutableArray<TSymbol> Locate<TSymbol>(this ConstraintContext context, NameNode name)
            where TSymbol : ISymbol
        {
            return context.Provider.GetRequiredService<IMetaConverter<NameNode, ISymbol>>()
                .Convert(name)
                .Cast<TSymbol>()
                .ToImmutableArray();
        }

        public static void Report(
            this ConstraintContext context,
            string message,
            IEnumerable<object> arguments,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<CSharpValidationReporter>().Report(message, arguments, symbol, severity);
        }

        public static void Report(
            this ConstraintContext context,
            string message,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<CSharpValidationReporter>().Report(message, symbol, severity);
        }
    }
}