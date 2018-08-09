using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : Validation.Unit.Unit
    {
        private readonly List<Action<CSharpDynamicContext>> dynamicActions = new List<Action<CSharpDynamicContext>>();

        public CSharpUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IEnumerable<Action<CSharpDynamicContext>> GetDynamicActions()
        {
            return dynamicActions;
        }

        public CSharpQuery<TSymbol> GetQuery<TSymbol>(string name)
            where TSymbol : ISymbol
        {
            var lexer = new NameLexer(name);
            var nameNode = new NameParser(lexer).Parse();
            var query = ActivatorUtilities.CreateInstance<CSharpQuery<TSymbol>>(Provider, nameNode);
            AddQuery(query);
            return query;
        }

        public void Run(Action<CSharpDynamicContext> action)
        {
            dynamicActions.Add(action);
        }
    }
}