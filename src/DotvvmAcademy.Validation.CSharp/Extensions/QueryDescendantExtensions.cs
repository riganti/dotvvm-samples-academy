using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryDescendantExtensions
    {
        public static IQuery<IEventSymbol> GetEvent(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetEvents(name).CountEquals(1);
        }

        public static IQuery<IEventSymbol> GetEvents(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IEventSymbol>(name);
        }

        public static IQuery<IFieldSymbol> GetField(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetFields(name).CountEquals(1);
        }

        public static IQuery<IFieldSymbol> GetFields(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IFieldSymbol>(name);
        }

        public static IQuery<IMethodSymbol> GetMethod(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetMethods(name).CountEquals(1);
        }

        public static IQuery<IMethodSymbol> GetMethods(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IMethodSymbol>(name);
        }

        public static IQuery<IPropertySymbol> GetProperties(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetMemberQuery<IPropertySymbol>(name);
        }

        public static IQuery<IPropertySymbol> GetProperty(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetProperties(name).CountEquals(1);
        }

        public static IQuery<ITypeSymbol> GetType(this IQuery<ITypeSymbol> query, string name)
        {
            return query.GetTypes(name).CountEquals(1);
        }

        public static IQuery<ITypeSymbol> GetTypes(this IQuery<ITypeSymbol> query, string name)
        {
            var csharpQuery = (CSharpQuery<ITypeSymbol>)query;
            var lexer = new NameLexer(name);
            var right = new NameParser(lexer).Parse();
            if (!(right is SimpleNameNode simple))
            {
                throw new ArgumentException($"Name '{name}' is not a simple name.");
            }
            var plusToken = new NameToken(NameTokenKind.Plus, null, -1, -1, true);
            var nameNode = new NestedTypeNameNode(csharpQuery.Name, simple, plusToken);
            var typeQuery = ActivatorUtilities.CreateInstance<CSharpQuery<ITypeSymbol>>(query.Unit.Provider, nameNode);
            query.Unit.AddQuery(typeQuery);
            return typeQuery;
        }

        private static IQuery<TSymbol> GetMemberQuery<TSymbol>(this IQuery<ITypeSymbol> query, string name)
        {
            var csharpQuery = (CSharpQuery<ITypeSymbol>)query;
            var memberToken = new NameToken(NameTokenKind.Identifier, name, 0, name.Length - 1);
            var colonColonToken = new NameToken(NameTokenKind.ColonColon, null, -1, -1, true);
            var member = new IdentifierNameNode(memberToken);
            var nameNode = new MemberNameNode(csharpQuery.Name, member, colonColonToken);
            var memberQuery = ActivatorUtilities.CreateInstance<CSharpQuery<TSymbol>>(query.Unit.Provider, nameNode);
            query.Unit.AddQuery(memberQuery);
            return memberQuery;
        }
    }
}