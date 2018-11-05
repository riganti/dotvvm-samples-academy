using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Constraints
    {
        public static CSharpQuery<TResult> AddQueryConstraint<TResult>(
            this CSharpQuery<TResult> query,
            Action<ConstraintContext, ImmutableArray<TResult>> action,
            bool overwrite = true)
            where TResult : ISymbol
        {
            query.Unit.Constraints.Add(new CSharpQueryConstraint<TResult>(action, query, overwrite));
            return query;
        }

        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var storage = context.Provider.GetRequiredService<AllowedSymbolStorage>();
                foreach (var symbol in result)
                {
                    storage.Allow(symbol);
                }
            });
        }

        public static CSharpQuery<TResult> CountEquals<TResult>(this CSharpQuery<TResult> query, int count)
            where TResult : ISymbol
        {
            query.Unit.Constraints.Add(new CSharpCountConstraint<TResult>(query.Name, count));
            return query;
        }

        public static CSharpQuery<TResult> HasAccessibility<TResult>(this CSharpQuery<TResult> query, Accessibility accessibility)
            where TResult : ISymbol
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var symbol in result)
                {
                    if (!accessibility.HasFlag(symbol.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: Resources.ERR_WrongAccessibility,
                            arguments: new object[] { symbol, accessibility },
                            symbol: symbol);
                    }
                }
            });
        }

        public static CSharpQuery<ITypeSymbol> HasBaseType<TBase>(this CSharpQuery<ITypeSymbol> query)
            where TBase : class
        {
            return query.HasBaseType(query.Unit.GetMetaName<TBase>());
        }

        public static CSharpQuery<ITypeSymbol> HasBaseType(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var baseType = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                foreach (var typeSymbol in result)
                {
                    if (!typeSymbol.BaseType.Equals(baseType))
                    {
                        context.Report(
                            message: Resources.ERR_WrongBaseType,
                            arguments: new object[] { typeSymbol, baseType },
                            symbol: typeSymbol);
                    }
                }
            });
        }

        public static CSharpQuery<IPropertySymbol> HasGetter(
            this CSharpQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var property in result)
                {
                    if (property.GetMethod == null
                        || !accessibility.HasFlag(property.GetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: Resources.ERR_MissingGetter,
                            arguments: new object[] { property, accessibility },
                            symbol: (ISymbol)property.GetMethod ?? property);
                    }
                }
            });
        }

        public static CSharpQuery<IPropertySymbol> HasSetter(
            this CSharpQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var property in result)
                {
                    if (property.SetMethod == null
                        || !accessibility.HasFlag(property.SetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: Resources.ERR_MissingSetter,
                            arguments: new object[] { property, accessibility },
                            symbol: (ISymbol)property.SetMethod ?? property);
                    }
                }
            });
        }

        public static CSharpQuery<ITypeSymbol> Implements<TInterface>(this CSharpQuery<ITypeSymbol> query)
        {
            return query.Implements(query.Unit.GetMetaName<TInterface>());
        }

        public static CSharpQuery<ITypeSymbol> Implements(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var interfaceSymbol = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                foreach (var typeSymbol in result)
                {
                    if (!typeSymbol.AllInterfaces.Contains(interfaceSymbol))
                    {
                        context.Report(
                            message: Resources.ERR_MissingInterfaceImplementation,
                            arguments: new object[] { typeSymbol, interfaceSymbol },
                            symbol: typeSymbol);
                    }
                }
            }, false);
        }

        public static CSharpQuery<IFieldSymbol> IsOfType<TType>(this CSharpQuery<IFieldSymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IFieldSymbol> IsOfType(this CSharpQuery<IFieldSymbol> query, string typeName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var type = context.Locate<IFieldSymbol>(NameNode.Parse(typeName)).Single();
                foreach (var field in result)
                {
                    if (!field.Type.Equals(type))
                    {
                        context.Report(
                            message: Resources.ERR_WrongFieldType,
                            arguments: new object[] { field, type },
                            symbol: field);
                    }
                }
            });
        }

        public static CSharpQuery<IPropertySymbol> IsOfType<TType>(this CSharpQuery<IPropertySymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IPropertySymbol> IsOfType(this CSharpQuery<IPropertySymbol> query, string typeName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var type = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).SingleOrDefault();
                if (type == null)
                {
                    return;
                }
                foreach (var property in result)
                {
                    if (!property.Type.Equals(type))
                    {
                        context.Report(
                            message: Resources.ERR_WrongPropertyType,
                            arguments: new object[] { property, type },
                            symbol: property);
                    }
                }
            });
        }

        public static CSharpQuery<IFieldSymbol> IsReadonly(this CSharpQuery<IFieldSymbol> query)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var field in result)
                {
                    if (!field.IsReadOnly)
                    {
                        context.Report(
                            message: Resources.ERR_MissingReadonly,
                            arguments: new object[] { field },
                            symbol: field);
                    }
                }
            });
        }

        public static CSharpQuery<ITypeSymbol> IsTypeKind(this CSharpQuery<ITypeSymbol> query, TypeKind typeKind)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var symbol in result)
                {
                    if (!typeKind.HasFlag(symbol.TypeKind.ToUnitTypeKind()))
                    {
                        context.Report(
                            message: Resources.ERR_WrongTypeKind,
                            arguments: new object[] { symbol, typeKind },
                            symbol: symbol);
                    }
                }
            });
        }

        public static CSharpQuery<IMethodSymbol> Returns<TType>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.Returns(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IMethodSymbol> Returns(this CSharpQuery<IMethodSymbol> query, string typeName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                var type = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                foreach (var method in result)
                {
                    if (!method.ReturnType.Equals(type))
                    {
                        context.Report(
                            message: Resources.ERR_WrongReturnType,
                            arguments: new object[] { method, type },
                            symbol: method);
                    }
                }
            });
        }
    }
}