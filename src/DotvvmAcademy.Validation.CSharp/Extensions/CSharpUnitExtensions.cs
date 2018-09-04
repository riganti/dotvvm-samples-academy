using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpUnitExtensions
    {
        public static CSharpQuery<IEventSymbol> GetEvent(this CSharpUnit unit, string name)
        {
            return unit.GetEvents(name).Exists();
        }

        public static CSharpQuery<IEventSymbol> GetEvents(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IEventSymbol>(name);
        }

        public static CSharpQuery<IFieldSymbol> GetField(this CSharpUnit unit, string name)
        {
            return unit.GetFields(name).Exists();
        }

        public static CSharpQuery<IFieldSymbol> GetFields(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IFieldSymbol>(name);
        }

        public static string GetMetaName<TType>(this CSharpUnit unit)
        {
            return unit.GetMetaName(typeof(TType));
        }

        public static string GetMetaName(this CSharpUnit unit, Type type)
        {
            return unit.Provider.GetRequiredService<IMemberInfoNameBuilder>().Build(type).ToString();
        }

        public static CSharpQuery<IMethodSymbol> GetMethod(this CSharpUnit unit, string name)
        {
            return unit.GetMethods(name).Exists();
        }

        public static CSharpQuery<IMethodSymbol> GetMethods(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IMethodSymbol>(name);
        }

        public static CSharpQuery<IPropertySymbol> GetProperties(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IPropertySymbol>(name);
        }

        public static CSharpQuery<IPropertySymbol> GetProperty(this CSharpUnit unit, string name)
        {
            return unit.GetProperties(name).Exists();
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpUnit unit, string name)
        {
            return unit.GetTypes(name).Exists();
        }

        public static CSharpQuery<ITypeSymbol> GetType<TType>(this CSharpUnit unit)
        {
            return unit.GetType(typeof(TType).FullName);
        }

        public static CSharpQuery<ITypeSymbol> GetTypes(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<ITypeSymbol>(name);
        }

        public static void Run(this CSharpUnit unit, Action<CSharpDynamicContext> action)
        {
            unit.DynamicActions.Add(action);
        }

        private static CSharpQuery<TResult> GetQuery<TResult>(this CSharpUnit unit, string name)
            where TResult : ISymbol
        {
            // It's not CSharpQuery's responsibility to parse the name
            return new CSharpQuery<TResult>(unit, NameNode.Parse(name));
        }
    }
}