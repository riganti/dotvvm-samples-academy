using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class AttributeExtractor : IAttributeExtractor
    {
        private readonly ITypedConstantExtractor constantExtractor;
        private readonly IMetaLocator<MemberInfo> locator;

        public AttributeExtractor(ITypedConstantExtractor constantExtractor, IMetaLocator<MemberInfo> locator)
        {
            this.constantExtractor = constantExtractor;
            this.locator = locator;
        }

        public Attribute Extract(AttributeData attributeData)
        {
            var arguments = attributeData.ConstructorArguments
                .Select(constantExtractor.Extract)
                .ToArray();
            var type = Type.GetType(FullNamer.FromRoslyn(
                type: attributeData.AttributeClass,
                qualification: Qualification.Assembly));
            var instance = Activator.CreateInstance(type, arguments);
            foreach (var namedArgument in attributeData.NamedArguments)
            {
                var property = type.GetProperty(namedArgument.Key);
                var argument = constantExtractor.Extract(namedArgument.Value);
                property.SetValue(instance, argument);
            }
            return (Attribute)instance;
        }

        //public ImmutableArray<AttributeData> GetAttributeData(Type attributeType, ISymbol symbol)
        //{
        //    var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(attributeType));
        //    return symbol.GetAttributes()
        //        .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
        //        .ToImmutableArray();
        //}

        //public ImmutableArray<Attribute> GetAttributes(Type attributeType, ISymbol symbol)
        //{
        //    var attributeTypeName = FullNamer.FromReflection(attributeType);
        //    var attributeSymbol = Compilation.GetTypeByMetadataName(attributeTypeName);
        //    if (attributeSymbol == null)
        //    {
        //        throw new ArgumentException($"INamedTypeSymbol of '{attributeTypeName}'" +
        //            $" could not be obtained.", nameof(attributeType));
        //    }

        //    return symbol.GetAttributes()
        //        .Where(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists)
        //        .Select(CreateAttribute)
        //        .ToImmutableArray();
        //}

        //public bool HasAttribute(Type attributeType, ISymbol symbol)
        //{
        //    var attributeSymbol = Compilation.GetTypeByMetadataName(FullNamer.FromReflection(attributeType));
        //    return symbol.GetAttributes()
        //        .Any(a => Compilation.ClassifyConversion(a.AttributeClass, attributeSymbol).Exists);
        //}
    }
}