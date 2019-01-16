using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp.Constraints;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Constraints
    {
        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            return query.AddOverwritableConstraint(new AllowConstraint<TResult>(query.Node));
        }

        public static CSharpQuery<ITypeSymbol> RequireInterface<TInterface>(this CSharpQuery<ITypeSymbol> query)
        {
            var interfaceNode = MetaConvert.ToMeta(typeof(TInterface));
            return query.AddOverwritableConstraint(new InterfaceConstraint(query.Node, interfaceNode), interfaceNode);
        }

        public static CSharpQuery<ITypeSymbol> RequireInterface(this CSharpQuery<ITypeSymbol> query, string @interface)
        {
            var interfaceNode = NameNode.Parse(@interface);
            return query.AddOverwritableConstraint(new InterfaceConstraint(query.Node, interfaceNode), interfaceNode);
        }

        public static CSharpQuery<ITypeSymbol> RequireConversion(this CSharpQuery<ITypeSymbol> query, string destination)
        {
            var destinationNode = NameNode.Parse(destination);
            return query.AddOverwritableConstraint(new ConversionConstraint(query.Node, destinationNode), destinationNode);
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

        public static CSharpQuery<IPropertySymbol> RequireType<TType>(this CSharpQuery<IPropertySymbol> query)
        {
            return query.AddOverwritableConstraint(new PropertyTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IPropertySymbol> RequireType(this CSharpQuery<IPropertySymbol> query, string typeName)
        {
            return query.AddOverwritableConstraint(new PropertyTypeConstraint(query.Node, NameNode.Parse(typeName)));
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

        public static CSharpQuery<TResult> RequireAccess<TResult>(this CSharpQuery<TResult> query, AllowedAccess access)
            where TResult : ISymbol
        {
            return query.AddOverwritableConstraint(new AccessConstraint<TResult>(query.Node, access));
        }

        public static CSharpQuery<ITypeSymbol> RequireBaseType<TBase>(this CSharpQuery<ITypeSymbol> query)
            where TBase : class
        {
            return query.AddOverwritableConstraint(new BaseTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TBase))));
        }

        public static CSharpQuery<ITypeSymbol> RequireBaseType(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            return query.AddOverwritableConstraint(new BaseTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<TResult> RequireCount<TResult>(this CSharpQuery<TResult> query, int count)
            where TResult : ISymbol
        {
            return query.AddOverwritableConstraint(new CountConstraint<TResult>(query.Node, count));
        }

        public static CSharpQuery<IPropertySymbol> RequireGetter(
            this CSharpQuery<IPropertySymbol> query,
            AllowedAccess access = AllowedAccess.Public)
        {
            return query.RequireAccessor("get", access);
        }

        public static CSharpQuery<IPropertySymbol> RequireSetter(
            this CSharpQuery<IPropertySymbol> query,
            AllowedAccess access = AllowedAccess.Public)
        {
            return query.RequireAccessor("set", access);
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

        private static CSharpQuery<IPropertySymbol> RequireAccessor(
                            this CSharpQuery<IPropertySymbol> query,
            string accessorKind,
            AllowedAccess access)
        {
            var member = (MemberNameNode)query.Node;
            var accessor = NameFactory.Member(member.Type, $"{accessorKind}_{member.Member.IdentifierToken.ToString()}");
            query.Unit.AddOverwritableConstraint(new CountConstraint<IMethodSymbol>(accessor, 1), accessor);
            query.Unit.AddOverwritableConstraint(new AccessConstraint<IMethodSymbol>(accessor, access), accessor);
            return query;
        }
    }
}