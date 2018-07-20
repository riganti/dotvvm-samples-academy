using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : IUnit
    {
        public ConcurrentDictionary<MetadataName, IQuery> Queries
            = new ConcurrentDictionary<MetadataName, IQuery>();

        public string CorrectCode { get; set; }

        public string DefaultCode { get; set; }

        public CSharpQuery<IEventSymbol> GetEvents(MetadataName name) => AddQuery<IEventSymbol>(name);

        public CSharpQuery<IFieldSymbol> GetFields(MetadataName name) => AddQuery<IFieldSymbol>(name);

        public CSharpQuery<IMethodSymbol> GetMethods(MetadataName name) => AddQuery<IMethodSymbol>(name);

        public CSharpQuery<IPropertySymbol> GetProperties(MetadataName name) => AddQuery<IPropertySymbol>(name);

        public CSharpQuery<ITypeSymbol> GetTypes(MetadataName name) => AddQuery<ITypeSymbol>(name);

        private CSharpQuery<TSymbol> AddQuery<TSymbol>(MetadataName name)
            where TSymbol : ISymbol
        {
            return (CSharpQuery<TSymbol>)Queries.GetOrAdd(name, n =>
                new CSharpQuery<TSymbol>(name));
        }
    }
}