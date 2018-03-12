using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class RoslynMetadataNameProvider : IMetadataNameProvider<ISymbol>
    {
        private readonly IMetadataNameFactory factory;

        public RoslynMetadataNameProvider(IMetadataNameFactory factory)
        {
            this.factory = factory;
        }

        public MetadataName GetName(ISymbol symbol)
        {
            switch (symbol)
            {
                case IEventSymbol @event:
                    return GetEventName(@event);

                case IFieldSymbol field:
                    return GetFieldName(field);

                case IMethodSymbol method:
                    return GetMethodName(method);

                case IPropertySymbol property:
                    return GetPropertyName(property);

                case ITypeSymbol type:
                    return GetTypeName(type);

                default:
                    throw new ArgumentException($"Symbol kind {symbol.Kind} is not supported by {nameof(RoslynMetadataNameProvider)}.");
            }
        }

        private MetadataName GetEventName(IEventSymbol @event)
        {
            var type = GetTypeName(@event.Type);
            var containingType = GetTypeName(@event.ContainingType);
            return factory.CreateFieldName(
                owner:containingType, 
                name: @event.Name,
                returnType: type);
        }

        private MetadataName GetFieldName(IFieldSymbol field)
        {
            var type = GetTypeName(field.Type);
            var containingType = GetTypeName(field.ContainingType);
            return factory.CreateFieldName(
                owner: containingType,
                name: field.Name,
                returnType: type);
        }

        private MetadataName GetMethodName(IMethodSymbol method)
        {
            if (method.ConstructedFrom != null)
            {
                var owner = GetMethodName(method.ConstructedFrom);
                var typeArguments = method.TypeArguments
                    .Select(tp => GetTypeName(tp))
                    .ToImmutableArray();
                return factory.CreateConstructedMethodName(
                    owner: owner,
                    typeArguments: typeArguments);
            }

            var returnType = GetTypeName(method.ReturnType);
            var containingType = GetTypeName(method.ContainingType);
            var parameters = method.Parameters.Select(p => GetTypeName(p.Type))
                .ToImmutableArray();
            return factory.CreateMethodName(
                owner: containingType,
                name: method.Name,
                returnType: returnType,
                parameters: parameters,
                arity: method.Arity);
        }

        private MetadataName GetNamedTypeName(INamedTypeSymbol namedType)
        {
            if (namedType.IsGenericType && !namedType.IsUnboundGenericType)
            {
                var genericArguments = namedType.TypeArguments
                    .Select(a => GetTypeName(a))
                    .ToImmutableArray();

                var owner = GetTypeName(namedType.ConstructedFrom);

                return factory.CreateConstructedTypeName(owner, genericArguments);
            }

            if (namedType.ContainingType != null)
            {
                var owner = GetTypeName(namedType.ContainingType);
                return factory.CreateNestedTypeName(
                    owner: owner,
                    name: namedType.Name,
                    arity: namedType.Arity);
            }

            return factory.CreateTypeName(
                @namespace: GetNamespaceName(namedType.ContainingNamespace),
                name: namedType.Name,
                arity: namedType.Arity);
        }

        private string GetNamespaceName(INamespaceSymbol namespaceSymbol)
        {
            if (namespaceSymbol.IsGlobalNamespace)
            {
                return "";
            }

            var ancestor = GetNamespaceName(namespaceSymbol.ContainingNamespace);
            return ancestor == string.Empty
                ? namespaceSymbol.Name
                : $"{ancestor}.{namespaceSymbol.Name}";
        }

        private MetadataName GetPropertyName(IPropertySymbol property)
        {
            var type = GetTypeName(property.Type);
            var containingType = GetTypeName(property.ContainingType);
            return factory.CreateMethodName(
                owner:containingType, 
                name: property.Name,
                returnType: type);
        }

        private MetadataName GetTypeName(ITypeSymbol type)
        {
            switch (type)
            {
                case IArrayTypeSymbol arrayType:
                    var owner = GetTypeName(arrayType.ElementType);
                    return factory.CreateArrayTypeName(owner, arrayType.Rank);

                case IPointerTypeSymbol pointerType:
                    owner = GetTypeName(pointerType.PointedAtType);
                    return factory.CreatePointerType(owner);

                case ITypeParameterSymbol typeParameter:
                    owner = GetName((ISymbol)typeParameter.DeclaringType ?? typeParameter.DeclaringMethod);
                    return factory.CreateTypeParameterName(owner, typeParameter.Name);

                case INamedTypeSymbol namedType:
                    return GetNamedTypeName(namedType);
            }

            return null;
        }
    }
}