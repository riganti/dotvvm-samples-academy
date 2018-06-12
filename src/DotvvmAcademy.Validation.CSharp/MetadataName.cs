using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class MetadataName : IEquatable<MetadataName>
    {
        private readonly Lazy<string> reflectionName;
        private readonly Lazy<string> userFriendlyName;

        public MetadataName(
            MetadataNameFormatter defaultFormatter,
            ReflectionMetadataNameFormatter reflectionFormatter,
            UserFriendlyMetadataNameFormatter userFriendlyFormatter,
            MetadataNameKind kind,
            MetadataName returnType = null,
            string @namespace = null,
            string name = null,
            int arity = 0,
            int rank = 0,
            MetadataName owner = null,
            ImmutableArray<MetadataName> typeArguments = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            reflectionName = new Lazy<string>(() => reflectionFormatter.Format(this));
            userFriendlyName = new Lazy<string>(() => userFriendlyFormatter.Format(this));
            Kind = kind;
            ReturnType = returnType;
            Namespace = @namespace;
            Name = name;
            Arity = arity;
            Rank = rank;
            Owner = owner;
            TypeArguments = typeArguments;
            Parameters = parameters;
            FullName = defaultFormatter.Format(this);
        }

        public int Arity { get; }

        public string FullName { get; }

        public MetadataNameKind Kind { get; }

        public string Name { get; }

        public string Namespace { get; }

        public MetadataName Owner { get; }

        public ImmutableArray<MetadataName> Parameters { get; }

        public int Rank { get; }

        public string ReflectionName => reflectionName.Value;

        public MetadataName ReturnType { get; }

        public ImmutableArray<MetadataName> TypeArguments { get; }

        public string UserFriendlyName => userFriendlyName.Value;

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

        public override string ToString()
        {
            return FullName;
        }
    }
}