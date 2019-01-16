using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp.Constraints;
using Microsoft.CodeAnalysis;
using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpUnitExtensions
    {
        public static CSharpQuery<IEventSymbol> GetEvent(this CSharpUnit unit, string name)
        {
            return unit.GetEvents(name).CountEquals(1);
        }

        public static CSharpQuery<IEventSymbol> GetEvents(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IEventSymbol>(name);
        }

        public static CSharpQuery<IFieldSymbol> GetField(this CSharpUnit unit, string name)
        {
            return unit.GetFields(name).CountEquals(1);
        }

        public static CSharpQuery<IFieldSymbol> GetFields(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IFieldSymbol>(name);
        }

        public static CSharpQuery<IMethodSymbol> GetMethod(this CSharpUnit unit, string name)
        {
            return unit.GetMethods(name).CountEquals(1);
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
            return unit.GetProperties(name).CountEquals(1);
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpUnit unit, Type type)
        {
            return GetType(unit, type.FullName);
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpUnit unit, string name)
        {
            return unit.GetTypes(name).CountEquals(1);
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
            unit.AddUniqueConstraint(new DynamicActionConstraint(action));
        }

        private static CSharpQuery<TResult> GetQuery<TResult>(this CSharpUnit unit, string name)
            where TResult : ISymbol
        {
            return new CSharpQuery<TResult>(unit, NameNode.Parse(name));
        }
    }
}