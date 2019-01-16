using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.CSharp.Constraints;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Constraints
    {
        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            return query.AddOverwritableConstraint(new AllowConstraint<TResult>(query.Node));
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

        public static CSharpQuery<ITypeSymbol> RequireConversion(this CSharpQuery<ITypeSymbol> query, string destination)
        {
            var destinationNode = NameNode.Parse(destination);
            return query.AddOverwritableConstraint(new ConversionConstraint(query.Node, destinationNode), destinationNode);
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

        public static CSharpQuery<IFieldSymbol> RequireReadonly(this CSharpQuery<IFieldSymbol> query)
        {
            return query.AddOverwritableConstraint(new ReadonlyConstraint(query.Node));
        }

        public static CSharpQuery<IMethodSymbol> RequireReturnType(this CSharpQuery<IMethodSymbol> query, string typeName)
        {
            return query.AddOverwritableConstraint(new ReturnTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<IMethodSymbol> RequireReturnType<TType>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.AddOverwritableConstraint(new ReturnTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IPropertySymbol> RequireSetter(
                            this CSharpQuery<IPropertySymbol> query,
            AllowedAccess access = AllowedAccess.Public)
        {
            return query.RequireAccessor("set", access);
        }

        public static CSharpQuery<IFieldSymbol> RequireType<TType>(this CSharpQuery<IFieldSymbol> query)
        {
            return query.AddOverwritableConstraint(new FieldTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IFieldSymbol> RequireType(this CSharpQuery<IFieldSymbol> query, string typeName)
        {
            return query.AddOverwritableConstraint(new FieldTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<IPropertySymbol> RequireType<TType>(this CSharpQuery<IPropertySymbol> query)
        {
            return query.AddOverwritableConstraint(new PropertyTypeConstraint(query.Node, MetaConvert.ToMeta(typeof(TType))));
        }

        public static CSharpQuery<IPropertySymbol> RequireType(this CSharpQuery<IPropertySymbol> query, string typeName)
        {
            return query.AddOverwritableConstraint(new PropertyTypeConstraint(query.Node, NameNode.Parse(typeName)));
        }

        public static CSharpQuery<ITypeSymbol> RequireTypeKind(this CSharpQuery<ITypeSymbol> query, AllowedTypeKind typeKind)
        {
            return query.AddOverwritableConstraint(new TypeKindConstraint(query.Node, typeKind));
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