using DotvvmAcademy.Meta;
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
            return unit.GetEvents(name).RequireCount(1);
        }

        public static CSharpQuery<IEventSymbol> GetEvents(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IEventSymbol>(NameNode.Parse(name));
        }

        public static CSharpQuery<IFieldSymbol> GetField(this CSharpUnit unit, string name)
        {
            return unit.GetFields(name).RequireCount(1);
        }

        public static CSharpQuery<IFieldSymbol> GetFields(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IFieldSymbol>(NameNode.Parse(name));
        }

        public static CSharpQuery<IMethodSymbol> GetMethod(this CSharpUnit unit, string name)
        {
            return unit.GetMethods(name).RequireCount(1);
        }

        public static CSharpQuery<IMethodSymbol> GetMethods(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IMethodSymbol>(NameNode.Parse(name));
        }

        public static CSharpQuery<IPropertySymbol> GetProperties(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IPropertySymbol>(NameNode.Parse(name));
        }

        public static CSharpQuery<IPropertySymbol> GetProperty(this CSharpUnit unit, string name)
        {
            return unit.GetProperties(name).RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpUnit unit, Type type)
        {
            return unit.GetTypes(type).RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpUnit unit, string name)
        {
            return unit.GetTypes(name).RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetType<TType>(this CSharpUnit unit)
        {
            return unit.GetTypes<TType>().RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetTypes(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<ITypeSymbol>(NameNode.Parse(name));
        }

        public static CSharpQuery<ITypeSymbol> GetTypes(this CSharpUnit unit, Type type)
        {
            return unit.GetQuery<ITypeSymbol>(MetaConvert.ToMeta(type));
        }

        public static CSharpQuery<ITypeSymbol> GetTypes<TType>(this CSharpUnit unit)
        {
            return unit.GetTypes(typeof(TType));
        }

        public static void Run(this CSharpUnit unit, Action<CSharpDynamicContext> action)
        {
            unit.AddConstraint(new DynamicActionConstraint(action), Guid.NewGuid());
        }

        private static CSharpQuery<TResult> GetQuery<TResult>(this CSharpUnit unit, NameNode node)
            where TResult : ISymbol
        {
            return new CSharpQuery<TResult>(unit, node);
        }
    }
}