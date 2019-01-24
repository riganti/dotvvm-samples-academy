using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp.Constraints;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Constraints
    {
        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            return query.AddConstraint(new AllowConstraint<TResult>(query.Node));
        }

        public static CSharpQuery<TResult> RequireAccess<TResult>(this CSharpQuery<TResult> query, AllowedAccess access)
            where TResult : ISymbol
        {
            return query.AddConstraint(new AccessConstraint<TResult>(query.Node, access));
        }

        public static CSharpQuery<TResult> RequireAttribute<TResult>(
            this CSharpQuery<TResult> query,
            Type attributeType,
            object values = null)
            where TResult : ISymbol
        {
            var attributeNode = MetaConvert.ToMeta(attributeType);
            return query.AddConstraint(new AttributeConstraint(query.Node, attributeNode, values), attributeNode);
        }

        public static CSharpQuery<TResult> RequireAttribute<TResult>(
            this CSharpQuery<TResult> query,
            string attributeType,
            object values = null)
            where TResult : ISymbol
        {
            var attributeNode = NameNode.Parse(attributeType);
            return query.AddConstraint(new AttributeConstraint(query.Node, attributeNode, values), attributeNode);
        }

        public static CSharpQuery<ITypeSymbol> RequireAttribute<TAttribute>(this CSharpQuery<ITypeSymbol> query, object values = null)
            where TAttribute : Attribute
        {
            return query.RequireAttribute(typeof(TAttribute), values);
        }

        public static CSharpQuery<IPropertySymbol> RequireAttribute<TAttribute>(this CSharpQuery<IPropertySymbol> query, object values = null)
            where TAttribute : Attribute
        {
            return query.RequireAttribute(typeof(TAttribute), values);
        }

        public static CSharpQuery<IFieldSymbol> RequireAttribute<TAttribute>(this CSharpQuery<IFieldSymbol> query, object values = null)
            where TAttribute : Attribute
        {
            return query.RequireAttribute(typeof(TAttribute), values);
        }

        public static CSharpQuery<IMethodSymbol> RequireAttribute<TAttribute>(this CSharpQuery<IMethodSymbol> query, object values = null)
            where TAttribute : Attribute
        {
            return query.RequireAttribute(typeof(TAttribute), values);
        }

        public static CSharpQuery<IEventSymbol> RequireAttribute<TAttribute>(this CSharpQuery<IEventSymbol> query, object values = null)
            where TAttribute : Attribute
        {
            return query.RequireAttribute(typeof(TAttribute), values);
        }

        public static CSharpQuery<ITypeSymbol> RequireBaseType<TBase>(this CSharpQuery<ITypeSymbol> query)
            where TBase : class
        {
            return query.AddConstraint(new BaseTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TBase))));
        }

        public static CSharpQuery<ITypeSymbol> RequireBaseType(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            return query.AddConstraint(new BaseTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<ITypeSymbol> RequireConversion(this CSharpQuery<ITypeSymbol> query, string destination)
        {
            var destinationNode = NameNode.Parse(destination);
            return query.AddConstraint(new ConversionConstraint(query.Node, destinationNode), destinationNode);
        }

        public static CSharpQuery<TResult> RequireCount<TResult>(this CSharpQuery<TResult> query, int count)
            where TResult : ISymbol
        {
            return query.AddConstraint(new CountConstraint<TResult>(query.Node, count));
        }

        public static CSharpQuery<IPropertySymbol> RequireGetter(
            this CSharpQuery<IPropertySymbol> query,
            AllowedAccess access = AllowedAccess.Public)
        {
            return query.RequireAccessor("get", access);
        }

        public static CSharpQuery<ITypeSymbol> RequireInterface<TInterface>(this CSharpQuery<ITypeSymbol> query)
        {
            var interfaceNode = MetaConvert.ToMeta(typeof(TInterface));
            return query.AddConstraint(new InterfaceConstraint(query.Node, interfaceNode), interfaceNode);
        }

        public static CSharpQuery<ITypeSymbol> RequireInterface(this CSharpQuery<ITypeSymbol> query, string @interface)
        {
            var interfaceNode = NameNode.Parse(@interface);
            return query.AddConstraint(new InterfaceConstraint(query.Node, interfaceNode), interfaceNode);
        }

        public static CSharpQuery<TResult> RequireNoAttribute<TResult>(this CSharpQuery<TResult> query, Type attributeType)
            where TResult : ISymbol
        {
            var attributeNode = MetaConvert.ToMeta(attributeType);
            return query.AddConstraint(new NoAttributeConstraint(query.Node, attributeNode), attributeNode);
        }

        public static CSharpQuery<TResult> RequireNoAttribute<TResult>(this CSharpQuery<TResult> query, string attributeType)
            where TResult : ISymbol
        {
            var attributeNode = NameNode.Parse(attributeType);
            return query.AddConstraint(new NoAttributeConstraint(query.Node, attributeNode), attributeNode);
        }

        public static CSharpQuery<ITypeSymbol> RequireNoAttribute<TAttribute>(this CSharpQuery<ITypeSymbol> query)
            where TAttribute : Attribute
        {
            return query.RequireNoAttribute(typeof(TAttribute));
        }

        public static CSharpQuery<IPropertySymbol> RequireNoAttribute<TAttribute>(this CSharpQuery<IPropertySymbol> query)
            where TAttribute : Attribute
        {
            return query.RequireNoAttribute(typeof(TAttribute));
        }

        public static CSharpQuery<IFieldSymbol> RequireNoAttribute<TAttribute>(this CSharpQuery<IFieldSymbol> query)
            where TAttribute : Attribute
        {
            return query.RequireNoAttribute(typeof(TAttribute));
        }

        public static CSharpQuery<IEventSymbol> RequireNoAttribute<TAttribute>(this CSharpQuery<IEventSymbol> query)
            where TAttribute : Attribute
        {
            return query.RequireNoAttribute(typeof(TAttribute));
        }

        public static CSharpQuery<IMethodSymbol> RequireNoAttribute<TAttribute>(this CSharpQuery<IMethodSymbol> query)
            where TAttribute : Attribute
        {
            return query.RequireNoAttribute(typeof(TAttribute));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameterless(this CSharpQuery<IMethodSymbol> query)
        {
            return query.AddConstraint(new ParametersConstraint(query.Node, Enumerable.Empty<NameNode>()));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3, T4>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3, T4, T5>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3, T4, T5, T6>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3, T4, T5, T6, T7>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters<T1, T2, T3, T4, T5, T6, T7, T8>(
            this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters(this CSharpQuery<IMethodSymbol> query, params string[] parameters)
        {
            return query.AddConstraint(new ParametersConstraint(query.Node, parameters.Select(NameNode.Parse)));
        }

        public static CSharpQuery<IMethodSymbol> RequireParameters(this CSharpQuery<IMethodSymbol> query, params Type[] parameters)
        {
            return query.AddConstraint(new ParametersConstraint(query.Node, parameters.Select(MetaConvert.ToMeta)));
        }

        public static CSharpQuery<IFieldSymbol> RequireReadonly(this CSharpQuery<IFieldSymbol> query)
        {
            return query.AddConstraint(new ReadonlyConstraint(query.Node));
        }

        public static CSharpQuery<IMethodSymbol> RequireReturnType(this CSharpQuery<IMethodSymbol> query, string typeName)
        {
            return query.AddConstraint(new ReturnTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<IMethodSymbol> RequireReturnType<TType>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.RequireReturnType(typeof(TType));
        }

        public static CSharpQuery<IMethodSymbol> RequireReturnType(this CSharpQuery<IMethodSymbol> query, Type returnType)
        {
            return query.AddConstraint(new ReturnTypeConstraint(query.Node, MetaConvert.ToMeta(returnType)));
        }

        public static CSharpQuery<IPropertySymbol> RequireSetter(
            this CSharpQuery<IPropertySymbol> query,
            AllowedAccess access = AllowedAccess.Public)
        {
            return query.RequireAccessor("set", access);
        }

        public static CSharpQuery<IFieldSymbol> RequireType<TType>(this CSharpQuery<IFieldSymbol> query)
        {
            return query.AddConstraint(new FieldTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IFieldSymbol> RequireType(this CSharpQuery<IFieldSymbol> query, string typeName)
        {
            return query.AddConstraint(new FieldTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<IPropertySymbol> RequireType<TType>(this CSharpQuery<IPropertySymbol> query)
        {
            return query.AddConstraint(new PropertyTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IPropertySymbol> RequireType(this CSharpQuery<IPropertySymbol> query, string typeName)
        {
            return query.AddConstraint(new PropertyTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<ITypeSymbol> RequireTypeKind(this CSharpQuery<ITypeSymbol> query, AllowedTypeKind typeKind)
        {
            return query.AddConstraint(new TypeKindConstraint(query.Node, typeKind));
        }

        private static CSharpQuery<IPropertySymbol> RequireAccessor(
            this CSharpQuery<IPropertySymbol> query,
            string accessorKind,
            AllowedAccess access)
        {
            var member = (MemberNameNode)query.Node;
            var accessor = NameFactory.Member(member.Type, $"{accessorKind}_{member.Member.IdentifierToken.ToString()}");
            query.Unit.AddConstraint(new CountConstraint<IMethodSymbol>(accessor, 1), accessor);
            query.Unit.AddConstraint(new AccessConstraint<IMethodSymbol>(accessor, access), accessor);
            return query;
        }
    }
}