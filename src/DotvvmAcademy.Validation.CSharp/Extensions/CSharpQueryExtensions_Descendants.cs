using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Descendants
    {
        public static CSharpQuery<IEventSymbol> GetEvent(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetEvents(name).RequireCount(1);
        }

        public static CSharpQuery<IEventSymbol> GetEvents(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IEventSymbol>(name);
        }

        public static CSharpQuery<IFieldSymbol> GetField(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetFields(name).RequireCount(1);
        }

        public static CSharpQuery<IFieldSymbol> GetFields(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IFieldSymbol>(name);
        }

        public static CSharpQuery<IMethodSymbol> GetMethod(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetMethods(name).RequireCount(1);
        }

        public static CSharpQuery<IMethodSymbol> GetMethods(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IMethodSymbol>(name);
        }

        public static CSharpQuery<IPropertySymbol> GetProperties(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IPropertySymbol>(name);
        }

        public static CSharpQuery<IPropertySymbol> GetProperty(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetProperties(name).RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetType(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetTypes(name).RequireCount(1);
        }

        public static CSharpQuery<ITypeSymbol> GetTypes(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return query.GetNestedTypeQuery(name);
        }

        private static CSharpQuery<TResult> GetMemberQuery<TResult>(this CSharpQuery<ITypeSymbol> query, string name)
            where TResult : ISymbol
        {
            return new CSharpQuery<TResult>(query.Unit, NameFactory.Member(query.Node, name));
        }

        private static CSharpQuery<ITypeSymbol> GetNestedTypeQuery(this CSharpQuery<ITypeSymbol> query, string name)
        {
            return new CSharpQuery<ITypeSymbol>(query.Unit, NameFactory.NestedType(query.Node, (SimpleNameNode)NameNode.Parse(name)));
        }
    }
}