using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static ImmutableArray<ISymbol> Locate(this ConstraintContext context, NameNode name)
        {
            return context.Provider.GetRequiredService<ISymbolLocator>().Locate(name);
        }

        public static ImmutableArray<TSymbol> Locate<TSymbol>(this ConstraintContext context, NameNode name)
            where TSymbol : ISymbol
        {
            return context.Provider.GetRequiredService<ISymbolLocator>().Locate<TSymbol>(name);
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