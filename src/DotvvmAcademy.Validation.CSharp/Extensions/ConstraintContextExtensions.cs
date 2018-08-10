using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        internal static TSymbol LocateSymbol<TResult, TSymbol>(this ConstraintContext<TResult> context, string name)
            where TSymbol : ISymbol
        {
            return (TSymbol)context.Provider.GetRequiredService<SymbolLocator>().LocateSingle(name);
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