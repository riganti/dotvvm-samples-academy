using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        internal static TSymbol LocateSymbol<TResult, TSymbol>(this ConstraintContext<TResult> context, string name)
            where TSymbol : ISymbol
        {
            return (TSymbol)context.Provider.GetRequiredService<SymbolLocator>().Locate(name).Single();
        }

        internal static void Report<TResult>(
            this ConstraintContext<TResult> context,
            string message,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<CSharpValidationReporter>().Report(message, symbol, severity);
        }
    }
}