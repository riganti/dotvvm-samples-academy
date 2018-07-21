using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IUnit
    {
        public string CorrectCode { get; set; }

        public string DefaultCode { get; set; }

        public List<Action<CSharpDynamicContext>> DynamicActions { get; } = new List<Action<CSharpDynamicContext>>();

        public ConcurrentDictionary<MetadataName, IQuery> Queries { get; }
            = new ConcurrentDictionary<MetadataName, IQuery>();

        public CSharpQuery<IEventSymbol> GetEvents(MetadataName name) => AddQuery<IEventSymbol>(name);

        public CSharpQuery<IFieldSymbol> GetFields(MetadataName name) => AddQuery<IFieldSymbol>(name);

        public CSharpQuery<IMethodSymbol> GetMethods(MetadataName name) => AddQuery<IMethodSymbol>(name);

        public CSharpQuery<IPropertySymbol> GetProperties(MetadataName name) => AddQuery<IPropertySymbol>(name);

        public CSharpQuery<ITypeSymbol> GetTypes(MetadataName name) => AddQuery<ITypeSymbol>(name);

        public void Run(Action<CSharpDynamicContext> action) => DynamicActions.Add(action);

        private CSharpQuery<TSymbol> AddQuery<TSymbol>(MetadataName name)
            where TSymbol : ISymbol
        {
            return (CSharpQuery<TSymbol>)Queries.GetOrAdd(name, n =>
                new CSharpQuery<TSymbol>(name));
        }
    }
}