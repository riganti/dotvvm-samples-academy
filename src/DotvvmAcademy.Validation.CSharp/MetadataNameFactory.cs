using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class MetadataNameFactory : IMetadataNameFactory
    {
        private readonly MetadataNameFormatter defaultFormatter;
        private readonly ReflectionMetadataNameFormatter reflectionFormatter;
        private readonly UserFriendlyMetadataNameFormatter userFriendlyFormatter;

        public MetadataNameFactory(MetadataNameFormatter defaultFormatter, ReflectionMetadataNameFormatter reflectionFormatter,
            UserFriendlyMetadataNameFormatter userFriendlyFormatter)
        {
            this.defaultFormatter = defaultFormatter;
            this.reflectionFormatter = reflectionFormatter;
            this.userFriendlyFormatter = userFriendlyFormatter;
        }

        public MetadataName CreateArrayTypeName(MetadataName owner, int rank = 1)
        {
            AssertIsType(owner, "The owner of an array type must be a type.");
            AssertRankNotNegative(rank);
            return Create(
                kind: MetadataNameKind.Type | MetadataNameKind.ArrayType,
                owner: owner,
                rank: rank);
        }

        public MetadataName CreateConstructedMethodName(MetadataName owner, ImmutableArray<MetadataName> typeArguments)
        {
            AssertIsMethod(owner, "The owner of a constructed method must be a method.");
            AssertValidTypeArguments(typeArguments, owner);
            return Create(
                kind: MetadataNameKind.Member | MetadataNameKind.Method | MetadataNameKind.ConstructedMethod,
                owner: owner,
                typeArguments: typeArguments);
        }

        public MetadataName CreateConstructedTypeName(MetadataName owner, ImmutableArray<MetadataName> typeArguments)
        {
            AssertIsType(owner, "The owner of a constructed type must be a type.");
            AssertValidTypeArguments(typeArguments, owner);
            return Create(
                kind: MetadataNameKind.Type | MetadataNameKind.ConstructedType,
                owner: owner,
                typeArguments: typeArguments);
        }

        public MetadataName CreateFieldName(MetadataName owner, string name, MetadataName returnType)
        {
            AssertIsType(owner, "The owner of a member must be a type.");
            AssertValidName(name);
            AssertValidReturnType(returnType);
            return Create(
                kind: MetadataNameKind.Member,
                returnType: returnType,
                name: name);
        }

        public MetadataName CreateMethodName(MetadataName owner, string name, MetadataName returnType = null,
            int arity = 0, ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            AssertIsType(owner, "The owner of a member must be a type.");
            AssertValidName(name);
            AssertValidReturnType(returnType);
            AssertValidArity(arity);
            AssertValidParameters(parameters);
            return Create(
                name: name,
                owner: owner,
                kind: MetadataNameKind.Member | MetadataNameKind.Method,
                returnType: returnType,
                arity: arity,
                parameters: parameters);
        }

        public MetadataName CreateNestedTypeName(MetadataName owner, string name, int arity = 0)
        {
            AssertIsType(owner, "The owner of nested type must be a type.");
            AssertValidName(name);
            AssertValidArity(arity);
            return Create(
                kind: MetadataNameKind.Type | MetadataNameKind.NestedType,
                owner: owner,
                name: name,
                arity: arity);
        }

        public MetadataName CreatePointerType(MetadataName owner)
        {
            AssertIsType(owner, "The owner of a pointer type must be a type.");
            return Create(
                kind: MetadataNameKind.Type | MetadataNameKind.PointerType,
                owner: owner);
        }

        public MetadataName CreateTypeName(string @namespace, string name, int arity = 0)
        {
            AssertValidName(name);
            AssertValidArity(arity);
            return Create(
                kind: MetadataNameKind.Type,
                @namespace: @namespace,
                name: name,
                arity: arity);
        }

        public MetadataName CreateTypeParameterName(MetadataName owner, string name)
        {
            AssertValidName(name);
            return Create(
                kind: MetadataNameKind.Type | MetadataNameKind.TypeParameter,
                owner: owner,
                name: name);
        }

        private static void AssertAreTypes(
            ImmutableArray<MetadataName> types,
            string notTypeMessage,
            int? expectedArity = null,
            string badArityMessage = null)
        {
            if (expectedArity != null && types.Length != expectedArity)
            {
                throw new InvalidOperationException(string.Format(badArityMessage, expectedArity, types.Length));
            }

            if (types.IsDefault || types.Length == 0)
            {
                return;
            }

            foreach (var type in types)
            {
                if (!type.Kind.HasFlag(MetadataNameKind.Type))
                {
                    throw new InvalidOperationException(notTypeMessage);
                }
            }
        }

        private static void AssertIsMethod(MetadataName name, string message)
        {
            if (!name.Kind.HasFlag(MetadataNameKind.Method))
            {
                throw new NotImplementedException(message);
            }
        }

        private static void AssertIsType(MetadataName name, string message)
        {
            if (!name.Kind.HasFlag(MetadataNameKind.Type))
            {
                throw new InvalidOperationException(message);
            }
        }

        private static void AssertValidArity(int arity)
        {
            if (arity < 0)
            {
                throw new InvalidOperationException("Arity must not be negative.");
            }
        }

        private static void AssertValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name must not be null, empty, or white-space.");
            }
        }

        private static void AssertValidParameters(ImmutableArray<MetadataName> parameters)
            => AssertAreTypes(types: parameters,
                notTypeMessage: "All parameters of a member must be types.");

        private static void AssertValidReturnType(MetadataName returnType)
            => AssertIsType(returnType, "The return type of a member must be a type.");

        private static void AssertValidTypeArguments(ImmutableArray<MetadataName> typeArguments, MetadataName owner)
            => AssertAreTypes(types: typeArguments,
                notTypeMessage: "All type arguments must be types.",
                expectedArity: owner.Arity,
                badArityMessage: "Expected {0} type arguments, got {1}.");

        private static void ValidateTypeParameters(ImmutableArray<MetadataName> typeParameters, MetadataName owner)
            => AssertAreTypes(types: typeParameters,
                notTypeMessage: "All type parameters of a member must be types.");

        private void AssertRankNotNegative(int rank)
        {
            if (rank <= 0)
            {
                throw new InvalidOperationException("The rank of an array item must be a positive integer.");
            }
        }

        private MetadataName Create(MetadataNameKind kind, MetadataName returnType = default(MetadataName), string @namespace = null,
            string name = null, int arity = 0, int rank = 0, MetadataName owner = default(MetadataName),
            ImmutableArray<MetadataName> typeArguments = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            return new MetadataName(defaultFormatter, reflectionFormatter, userFriendlyFormatter, kind, returnType,
                @namespace, name, arity, rank, owner, typeArguments, parameters);
        }
    }
}