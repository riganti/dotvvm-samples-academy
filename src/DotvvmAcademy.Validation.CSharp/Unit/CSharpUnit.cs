using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IUnit
    {
        public CSharpUnit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public List<Action<CSharpDynamicContext>> DynamicActions { get; } = new List<Action<CSharpDynamicContext>>();

        public ConcurrentDictionary<string, IQuery> Queries { get; }
            = new ConcurrentDictionary<string, IQuery>();

        public IServiceProvider Provider { get; }

        public CSharpQuery<IEventSymbol> GetEvents(string name) => AddQuery<IEventSymbol>(name);

        public CSharpQuery<IFieldSymbol> GetFields(string name) => AddQuery<IFieldSymbol>(name);

        public CSharpQuery<IMethodSymbol> GetMethods(string name) => AddQuery<IMethodSymbol>(name);

        public CSharpQuery<IPropertySymbol> GetProperties(string name) => AddQuery<IPropertySymbol>(name);

        public CSharpQuery<ITypeSymbol> GetTypes(string name) => AddQuery<ITypeSymbol>(name);

        public void Run(Action<CSharpDynamicContext> action) => DynamicActions.Add(action);

        private CSharpQuery<TSymbol> AddQuery<TSymbol>(string name)
            where TSymbol : ISymbol
        {
            return (CSharpQuery<TSymbol>)Queries.GetOrAdd(name, n =>
                new CSharpQuery<TSymbol>(name));
        }
    }
}