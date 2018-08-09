using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpUnitExtensions
    {
        public static IQuery<IEventSymbol> GetEvent(this CSharpUnit unit, string name)
        {
            return unit.GetEvents(name).CountEquals(1);
        }

        public static IQuery<IEventSymbol> GetEvents(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IEventSymbol>(name);
        }

        public static IQuery<IFieldSymbol> GetField(this CSharpUnit unit, string name)
        {
            return unit.GetFields(name).CountEquals(1);
        }

        public static IQuery<IFieldSymbol> GetFields(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IFieldSymbol>(name);
        }

        public static IQuery<IMethodSymbol> GetMethod(this CSharpUnit unit, string name)
        {
            return unit.GetMethods(name).CountEquals(1);
        }

        public static IQuery<IMethodSymbol> GetMethods(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IMethodSymbol>(name);
        }

        public static IQuery<IPropertySymbol> GetProperties(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<IPropertySymbol>(name);
        }

        public static IQuery<IPropertySymbol> GetProperty(this CSharpUnit unit, string name)
        {
            return unit.GetProperties(name).CountEquals(1);
        }

        public static IQuery<ITypeSymbol> GetType(this CSharpUnit unit, string name)
        {
            return unit.GetTypes(name).CountEquals(1);
        }

        public static IQuery<ITypeSymbol> GetType<TType>(this CSharpUnit unit)
        {
            return unit.GetType(typeof(TType).FullName);
        }

        public static IQuery<ITypeSymbol> GetTypes(this CSharpUnit unit, string name)
        {
            return unit.GetQuery<ITypeSymbol>(name);
        }
    }
}