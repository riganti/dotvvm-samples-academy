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

        public TAttribute GetAttribute<TAttribute>(ISymbol symbol)
            where TAttribute : Attribute
        {
            return GetAttributes<TAttribute>(symbol).FirstOrDefault();
        }

        public ImmutableArray<AttributeData> GetAttributeData<TAttribute>(ISymbol symbol)
            where TAttribute : Attribute
        {
            var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(typeof(TAttribute)));
            return symbol.GetAttributes()
                .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
                .ToImmutableArray();
        }

        public ImmutableArray<TAttribute> GetAttributes<TAttribute>(ISymbol symbol)
            where TAttribute : Attribute
        {
            var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(typeof(TAttribute)));
            if (attributeSymbol == null)
            {
                throw new ArgumentException($"INamedTypeSymbol of '{FullNamer.FromReflection(typeof(TAttribute))}'" +
                    $" could not be obtained.", nameof(TAttribute));
            }
            return symbol.GetAttributes()
                .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
                .Select(CreateAttribute)
                .Cast<TAttribute>()
                .ToImmutableArray();
        }

        public AttributeData GetFirstAttributeData<TAttribute>(ISymbol symbol)
            where TAttribute : Attribute
        {
            return GetAttributeData<TAttribute>(symbol).FirstOrDefault();
        }

        public bool HasAttribute<TAttribute>(ISymbol symbol)
            where TAttribute : Attribute
        {
            var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(typeof(TAttribute)));
            return symbol.GetAttributes()
                .Any(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists);
        }
    }
}