using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed partial class MetadataName : IEquatable<MetadataName>
    {
        private readonly BuilderFunc build;

        private MetadataName(BuilderFunc build,
            MetadataName returnType = null,
            string @namespace = null,
            string name = null,
            int arity = 0,
            MetadataName owner = null,
            ImmutableArray<MetadataName> genericArguments = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> genericParameters = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            this.build = build;
            ReturnType = returnType;
            Namespace = @namespace;
            Name = name;
            Arity = arity;
            Owner = owner;
            GenericArguments = genericArguments;
            GenericParameters = genericParameters;
            Parameters = parameters;
            FullName = build(DefaultConvention);
            ReflectionName = build(ReflectionConvention);
        }

        private delegate string BuilderFunc(NamingConvention convention);

        public int Arity { get; }

        public string FullName { get; }

        public ImmutableArray<MetadataName> GenericArguments { get; }

        public ImmutableArray<MetadataName> GenericParameters { get; }

        public bool IsArrayType { get; private set; }

        public bool IsConstructedType { get; private set; }

        public bool IsGenericParameterType { get; private set; }

        public bool IsMember { get; private set; }

        public bool IsNestedType { get; private set; }

        public bool IsPointerType { get; private set; }

        public bool IsType { get; private set; }

        public string Name { get; }

        public string Namespace { get; }

        public MetadataName Owner { get; }

        public ImmutableArray<MetadataName> Parameters { get; }

        public string ReflectionName { get; }

        public MetadataName ReturnType { get; }

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