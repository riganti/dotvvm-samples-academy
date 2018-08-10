using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class AttributeExtractor
    {
        public AttributeExtractor(CSharpCompilation compilation)
        {
            Compilation = compilation;
        }

        public CSharpCompilation Compilation { get; }

        public object CreateArgument(TypedConstant constant)
        {
            if (constant.IsNull)
            {
                return null;
            }

            switch (constant.Kind)
            {
                case TypedConstantKind.Enum:
                case TypedConstantKind.Primitive:
                    return constant.Value;

                case TypedConstantKind.Type:
                    return Type.GetType(FullNamer.FromRoslyn((ITypeSymbol)constant.Value, Qualification.Assembly));

                case TypedConstantKind.Array:
                    return constant.Values.Select(CreateArgument).ToArray();

                default:
                    throw new ArgumentException("Could not create passed TypedConstant.", nameof(constant));
            }
        }

        public Attribute CreateAttribute(AttributeData data)
        {
            var arguments = data.ConstructorArguments.Select(CreateArgument).ToArray();
            var type = Type.GetType(FullNamer.FromRoslyn(
                type: data.AttributeClass,
                qualification: Qualification.Assembly));
            var instance = Activator.CreateInstance(type, arguments);
            foreach (var namedArgument in data.NamedArguments)
            {
                var property = type.GetProperty(namedArgument.Key);
                var argument = CreateArgument(namedArgument.Value);
                property.SetValue(instance, argument);
            }
            return (Attribute)instance;
        }

        public ImmutableArray<AttributeData> GetAttributeData(Type attributeType, ISymbol symbol)
        {
            var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(attributeType));
            return symbol.GetAttributes()
                .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
                .ToImmutableArray();
        }

        public ImmutableArray<Attribute> GetAttributes(Type attributeType, ISymbol symbol)
        {
            var attributeTypeName = FullNamer.FromReflection(attributeType);
            var attributeSymbol = Compilation.GetTypeByMetadataName(attributeTypeName);
            if (attributeSymbol == null)
            {
                throw new ArgumentException($"INamedTypeSymbol of '{attributeTypeName}'" +
                    $" could not be obtained.", nameof(attributeType));
            }

            return symbol.GetAttributes()
                .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
                .Select(CreateAttribute)
                .ToImmutableArray();
        }

        public bool HasAttribute(Type attributeType, ISymbol symbol)
        {
            var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(attributeType));
            return symbol.GetAttributes()
                .Any(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists);
        }
    }
}