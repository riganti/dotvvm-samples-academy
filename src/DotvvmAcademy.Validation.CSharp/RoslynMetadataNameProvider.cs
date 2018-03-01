using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class RoslynMetadataNameProvider : IMetadataNameProvider<ISymbol>
    {
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
            return MetadataName.CreateFieldOrEventName(type, containingType, @event.Name);
        }

        private MetadataName GetFieldName(IFieldSymbol field)
        {
            var type = GetTypeName(field.Type);
            var containingType = GetTypeName(field.ContainingType);
            return MetadataName.CreateFieldOrEventName(type, containingType, field.Name);
        }

        private MetadataName GetMethodName(IMethodSymbol method)
        {
            var returnType = GetTypeName(method.ReturnType);
            var containingType = GetTypeName(method.ContainingType);
            var typeParameters = method.TypeParameters.Select(tp => GetTypeName(tp))
                .ToImmutableArray();
            var parameters = method.Parameters.Select(p => GetTypeName(p.Type))
                .ToImmutableArray();
            return MetadataName.CreateMethodName(
                owner: containingType,
                name: method.Name,
                returnType: returnType,
                typeParameters: typeParameters,
                parameters: parameters);
        }

        private MetadataName GetNamedTypeName(INamedTypeSymbol namedType)
        {
            if (namedType.IsGenericType && !namedType.IsUnboundGenericType)
            {
                var genericArguments = namedType.TypeArguments
                    .Select(a => GetTypeName(a))
                    .ToImmutableArray();

                var owner = GetTypeName(namedType.ConstructedFrom);

                return MetadataName.CreateConstructedTypeName(owner, genericArguments);
            }

            if (namedType.ContainingType != null)
            {
                var owner = GetTypeName(namedType.ContainingType);
                return MetadataName.CreateNestedTypeName(
                    owner: owner,
                    name: namedType.Name,
                    arity: namedType.Arity);
            }

            return MetadataName.CreateTypeName(
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
            return MetadataName.CreatePropertyName(type, containingType, property.Name);
        }

        private MetadataName GetTypeName(ITypeSymbol type)
        {
            switch (type.TypeKind)
            {
                case TypeKind.Array:
                    var owner = GetTypeName(type.OriginalDefinition);
                    return MetadataName.CreateArrayTypeName(owner);

                case TypeKind.Pointer:
                    owner = GetTypeName(type.OriginalDefinition);
                    return MetadataName.CreatePointerType(owner);

                case TypeKind.TypeParameter:
                    return MetadataName.CreateTypeParameterName(type.Name);

                default:
                    if (type is INamedTypeSymbol namedType)
                    {
                        return GetNamedTypeName(namedType);
                    }

                    return null;
            }
        }
    }
}