using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : Validation.Unit.Unit
    {
        private List<Action<CSharpDynamicContext>> dynamicActions = new List<Action<CSharpDynamicContext>>();

        public CSharpUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IEnumerable<Action<CSharpDynamicContext>> GetDynamicActions()
        {
            return dynamicActions;
        }

        public IQuery<IEventSymbol> GetEvents(string name) => GetQuery<IEventSymbol>(name);

        public IQuery<IFieldSymbol> GetFields(string name) => GetQuery<IFieldSymbol>(name);

        public IQuery<IMethodSymbol> GetMethods(string name) => GetQuery<IMethodSymbol>(name);

        public IQuery<IPropertySymbol> GetProperties(string name) => GetQuery<IPropertySymbol>(name);

        public IQuery<ITypeSymbol> GetTypes(string name) => GetQuery<ITypeSymbol>(name);

        public void Run(Action<CSharpDynamicContext> action) => dynamicActions.Add(action);
    }
}