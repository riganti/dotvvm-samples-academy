using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class MetadataName : IEquatable<MetadataName>
    {
        private readonly Lazy<string> fullName;
        private readonly Lazy<string> reflectionName;

        public MetadataName(
            MetadataNameKind kind,
            MetadataName returnType = null,
            string @namespace = null,
            string name = null,
            int arity = 0,
            int rank = 0,
            MetadataName owner = null,
            ImmutableArray<MetadataName> typeArguments = default,
            ImmutableArray<MetadataName> parameters = default)
        {
            Kind = kind;
            ReturnType = returnType;
            Namespace = @namespace;
            Name = name;
            Arity = arity;
            Rank = rank;
            Owner = owner;
            TypeArguments = typeArguments;
            Parameters = parameters;
            fullName = new Lazy<string>(() => new MetadataNameFormatter().Format(this));
            reflectionName = new Lazy<string>(() => new ReflectionMetadataNameFormatter().Format(this));
        }

        public int Arity { get; }

        public string FullName => fullName.Value;

        public MetadataNameKind Kind { get; }

        public string Name { get; }

        public string Namespace { get; }

        public MetadataName Owner { get; }

        public ImmutableArray<MetadataName> Parameters { get; }

        public int Rank { get; }

        public string ReflectionName => reflectionName.Value;

        public MetadataName ReturnType { get; }

        public ImmutableArray<MetadataName> TypeArguments { get; }

        public static bool operator !=(MetadataName name1, MetadataName name2) => !(name1 == name2);

        public static bool operator ==(MetadataName name1, MetadataName name2)
        {
            return EqualityComparer<MetadataName>.Default.Equals(name1, name2);
        }

        public override bool Equals(object obj) => Equals(obj as MetadataName);

        public bool Equals(MetadataName other) => other != null && FullName == other.FullName;

        public override int GetHashCode()
        {
            return 733961487 + EqualityComparer<string>.Default.GetHashCode(FullName);
        }

        public override string ToString() => FullName;
    }
}