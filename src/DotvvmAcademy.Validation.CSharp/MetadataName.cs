using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed partial class MetadataName : IEquatable<MetadataName>
    {
        private static ImmutableArray<Type> supportedSymbols
            = ImmutableArray.Create(
                typeof(ITypeSymbol),
                typeof(IMethodSymbol),
                typeof(IPropertySymbol),
                typeof(IEventSymbol),
                typeof(IFieldSymbol));

        private readonly BuilderFunc build;

        private MetadataName(BuilderFunc build,
            MetadataName returnType = null,
            string @namespace = null,
            string name = null,
            int arity = 0,
            MetadataName owner = null,
            ImmutableArray<MetadataName> typeArguments = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> typeParameters = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            this.build = build;
            ReturnType = returnType;
            Namespace = @namespace;
            Name = name;
            Arity = arity;
            Owner = owner;
            TypeArguments = typeArguments;
            TypeParameters = typeParameters;
            Parameters = parameters;
            FullName = build(DefaultConvention);
            ReflectionName = build(ReflectionConvention);
        }

        private delegate string BuilderFunc(NamingConvention convention);

        public int Arity { get; }

        public string FullName { get; }

        public bool IsArrayType { get; private set; }

        public bool IsConstructedType { get; private set; }

        public bool IsMember { get; private set; }

        public bool IsNestedType { get; private set; }

        public bool IsPointerType { get; private set; }

        public bool IsType { get; private set; }

        public bool IsTypeParameter { get; private set; }

        public string Name { get; }

        public string Namespace { get; }

        public MetadataName Owner { get; }

        public ImmutableArray<MetadataName> Parameters { get; }

        public string ReflectionName { get; }

        public MetadataName ReturnType { get; }

        public ImmutableArray<MetadataName> TypeArguments { get; }

        public ImmutableArray<MetadataName> TypeParameters { get; }

        public static bool IsSupportedSymbol(ISymbol symbol)
            => IsSupportedSymbol(symbol.GetType());

        public static bool IsSupportedSymbol<TSymbol>()
            where TSymbol : ISymbol
            => IsSupportedSymbol(typeof(TSymbol));

        public static bool IsSupportedSymbol(Type symbolType)
        {
            return supportedSymbols.Any(s => s.IsAssignableFrom(symbolType));
        }

        public static bool operator !=(MetadataName name1, MetadataName name2)
        {
            return !name1.Equals(name2);
        }

        public static bool operator ==(MetadataName name1, MetadataName name2)
        {
            return name1.Equals(name2);
        }

        public override bool Equals(object obj)
        {
            if (obj is MetadataName other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(MetadataName other)
        {
            return FullName.Equals(other.FullName);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public string ToString(NamingConvention convention)
        {
            return build(convention);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}