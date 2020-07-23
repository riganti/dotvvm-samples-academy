using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationPropertyFactory
    {
        private readonly Compilation compilation;
        private readonly IAttributeExtractor extractor;

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), IPropertyGroupDescriptor> groups
            = new ConcurrentDictionary<(INamedTypeSymbol, string), IPropertyGroupDescriptor>();

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), IPropertyDescriptor> properties
            = new ConcurrentDictionary<(INamedTypeSymbol, string), IPropertyDescriptor>();

        private readonly ValidationTypeDescriptorFactory typeDescriptorFactory;

        public ValidationPropertyFactory(
            ValidationTypeDescriptorFactory typeDescriptorFactory,
            Compilation compilation,
            IAttributeExtractor extractor)
        {
            this.typeDescriptorFactory = typeDescriptorFactory;
            this.compilation = compilation;
            this.extractor = extractor;
        }

        public IPropertyDescriptor Convert(IPropertyDescriptor property)
        {
            switch (property)
            {
                case ValidationProperty _:
                case ValidationAttachedProperty _:
                case ValidationVirtualProperty _:
                case ValidationGroupedProperty _:
                    return property;

                case GroupedDotvvmProperty groupedDotvvmProperty:
                    return CreateGrouped(
                        propertyGroup: (ValidationPropertyGroup)Convert(groupedDotvvmProperty.PropertyGroup),
                        groupMemberName: groupedDotvvmProperty.GroupMemberName);

                case DotvvmProperty dotvvmProperty:
                    var containingType = typeDescriptorFactory.Create(dotvvmProperty.DeclaringType).TypeSymbol;
                    var propertySymbol = containingType
                        .GetMembers(property.Name)
                        .OfType<IPropertySymbol>()
                        .SingleOrDefault();
                    var fieldSymbol = containingType
                        .GetMembers($"{property.Name}Property")
                        .OfType<IFieldSymbol>()
                        .SingleOrDefault();
                    if (propertySymbol != null && fieldSymbol != null)
                    {
                        return Create(propertySymbol, fieldSymbol);
                    }

                    if (propertySymbol != null)
                    {
                        return CreateVirtual(propertySymbol);
                    }

                    if (fieldSymbol != null)
                    {
                        return CreateAttached(fieldSymbol);
                    }

                    throw new ArgumentException($"DotvvmProperty '{property}' could not be converted.");

                default:
                    throw new ArgumentException($"Passed IPropertyDescriptor is of unknown type: '{property.GetType()}'.");
            }
        }

        public IPropertyGroupDescriptor Convert(IPropertyGroupDescriptor group)
        {
            switch (group)
            {
                case ValidationPropertyGroup _:
                case ValidationCollectionPropertyGroup _:
                    return group;

                case DotvvmPropertyGroup dotvvmGroup:
                    var containingType = typeDescriptorFactory.Create(dotvvmGroup.DeclaringType);
                    var generatorCandidate = containingType.TypeSymbol
                        .GetMembers($"{group.Name}PropertyGroup")
                        .OfType<IFieldSymbol>()
                        .SingleOrDefault();
                    var collectionCandidate = containingType.TypeSymbol
                        .GetMembers(group.Name)
                        .OfType<IPropertySymbol>()
                        .SingleOrDefault();
                    if (generatorCandidate != null && collectionCandidate != null)
                    {
                        return CreateGroup(collectionCandidate, generatorCandidate);
                    }

                    if (collectionCandidate != null)
                    {
                        return CreateCollectionGroup(collectionCandidate);
                    }

                    throw new ArgumentException($"DotvvmPropertyGroup '{group}' could not be converted.");

                default:
                    throw new ArgumentException($"Passed IPropertyGroupDescriptor is of unknown type: '{group.GetType()}'.");
            }
        }

        public IPropertyDescriptor CreateGrouped(IPropertyGroupDescriptor propertyGroup, string groupMemberName)
        {
            var name = $"{propertyGroup.Name}:{groupMemberName}";
            var containingSymbol = (INamedTypeSymbol)((ValidationTypeDescriptor)propertyGroup.DeclaringType).TypeSymbol;
            return properties.GetOrAdd((containingSymbol, name), _ =>
            {
                return new ValidationGroupedProperty(propertyGroup, groupMemberName);
            });
        }

        public ImmutableArray<IPropertyGroupDescriptor> GetGroups(ITypeSymbol containingType)
        {
            if (containingType.BaseType == null)
            {
                // end of recursion through base types
                return ImmutableArray.Create<IPropertyGroupDescriptor>();
            }

            var groupSymbol = compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmPropertyGroup);
            var builder = ImmutableArray.CreateBuilder<IPropertyGroupDescriptor>();
            builder.AddRange(GetGroups(containingType.BaseType));
            var collectionGroups = containingType.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => extractor.HasAttribute<PropertyGroupAttribute>(p));
            foreach (var group in collectionGroups)
            {
                builder.Add(CreateCollectionGroup(group));
            }

            // TODO: Get generator groups.

            return builder.ToImmutable();
        }

        public ImmutableArray<IPropertyDescriptor> GetProperties(ITypeSymbol containingType)
        {
            if (containingType.BaseType == null)
            {
                return ImmutableArray.Create<IPropertyDescriptor>();
            }

            var dotvvmPropertySymbol = compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmProperty);
            var builder = ImmutableArray.CreateBuilder<IPropertyDescriptor>();
            builder.AddRange(GetProperties(containingType.BaseType));
            var fields = containingType.GetMembers()
                .OfType<IFieldSymbol>()
                .Where(f => f.IsStatic
                    && f.MetadataName.EndsWith("Property")
                    && compilation.ClassifyConversion(f.Type, dotvvmPropertySymbol).Exists);
            foreach (var field in fields)
            {
                if (extractor.HasAttribute<AttachedPropertyAttribute>(field))
                {
                    builder.Add(CreateAttached(field));
                }
                else
                {
                    var propertyName = ValidationAttachedProperty.SanitizeName(field.MetadataName);
                    var propertyCounterpart = containingType.GetMembers(propertyName)
                        .OfType<IPropertySymbol>()
                        .SingleOrDefault();
                    if (propertyCounterpart == null)
                    {
                        continue;
                    }

                    builder.Add(Create(propertyCounterpart, field));
                }
            }
            var virtualProperties = containingType.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => p.DeclaredAccessibility == Accessibility.Public
                    && extractor.HasAttribute<MarkupOptionsAttribute>(p)
                    && !extractor.HasAttribute<PropertyGroupAttribute>(p)
                    && !properties.ContainsKey((p.ContainingType, p.MetadataName)));
            foreach (var property in virtualProperties)
            {
                builder.Add(CreateVirtual(property));
            }
            return builder.ToImmutable();
        }

        private IPropertyDescriptor Create(IPropertySymbol propertySymbol, IFieldSymbol fieldSymbol)
        {
            return properties.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                return new ValidationProperty(
                    propertySymbol: propertySymbol,
                    fieldSymbol: fieldSymbol,
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
                    markupOptions: GetMarkupOptionsAttribute(propertySymbol, propertySymbol.Name),
                    dataContextChangeAttributes: GetDataContextChangeAttributes(propertySymbol),
                    dataContextManipulationAttribute: GetDataContextManipulationAttribute(propertySymbol));
            });
        }

        private IPropertyDescriptor CreateAttached(IFieldSymbol fieldSymbol)
        {
            var name = ValidationAttachedProperty.SanitizeName(fieldSymbol.MetadataName);
            return properties.GetOrAdd((fieldSymbol.ContainingType, name), _ =>
            {
                var attachedAttribute = extractor.ExtractAttributeData<AttachedPropertyAttribute>(fieldSymbol).SingleOrDefault();
                if (attachedAttribute == null
                    || attachedAttribute.ConstructorArguments.Length != 1
                    || attachedAttribute.ConstructorArguments[0].Kind != TypedConstantKind.Type)
                {
                    throw new ArgumentException($"'{fieldSymbol}' does not have a valid AttachedPropertyAttribute.");
                }
                var propertyType = (ITypeSymbol)attachedAttribute.ConstructorArguments[0].Value;
                return new ValidationAttachedProperty(
                    fieldSymbol: fieldSymbol,
                    declaringType: typeDescriptorFactory.Create(fieldSymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertyType),
                    markupOptions: GetMarkupOptionsAttribute(fieldSymbol, name),
                    dataContextChangeAttributes: GetDataContextChangeAttributes(fieldSymbol),
                    dataContextManipulationAttribute: GetDataContextManipulationAttribute(fieldSymbol));
            });
        }

        private IPropertyGroupDescriptor CreateCollectionGroup(IPropertySymbol propertySymbol)
        {
            return (ValidationCollectionPropertyGroup)groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                return new ValidationCollectionPropertyGroup(
                    propertySymbol: propertySymbol,
                    propertyType: typeDescriptorFactory.Create(GetStringPairValueType(propertySymbol.Type)),
                    collectionType: typeDescriptorFactory.Create(propertySymbol.Type),
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    markupOptions: GetMarkupOptionsAttribute(propertySymbol),
                    dataContextChangeAttributes: GetDataContextChangeAttributes(propertySymbol),
                    dataContextManipulationAttribute: GetDataContextManipulationAttribute(propertySymbol),
                    propertyGroupAttribute: GetPropertyGroupAttribute(propertySymbol));
            });
        }

        private IPropertyGroupDescriptor CreateGroup(IPropertySymbol propertySymbol, IFieldSymbol fieldSymbol)
        {
            throw new NotImplementedException("There is currently no way to obtain the prefixes in the field call.");
            //return groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            //{
            //    return new ValidationPropertyGroup(
            //        propertySymbol: propertySymbol,
            //        fieldSymbol: fieldSymbol,
            //        declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
            //        propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
            //        markupOptions: GetMarkupOptionsAttribute(propertySymbol),
            //        dataContextChangeAttributes: GetDataContextChangeAttributes(propertySymbol),
            //        dataContextManipulationAttribute: GetDataContextManipulationAttribute(propertySymbol),
            //        prefixes: default);
            //});
        }

        private IPropertyDescriptor CreateVirtual(IPropertySymbol propertySymbol)
        {
            return properties.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                return new ValidationVirtualProperty(
                    propertySymbol: propertySymbol,
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
                    markupOptions: GetMarkupOptionsAttribute(propertySymbol),
                    dataContextChangeAttributes: GetDataContextChangeAttributes(propertySymbol),
                    dataContextManipulationAttribute: GetDataContextManipulationAttribute(propertySymbol));
            });
        }

        private ImmutableArray<DataContextChangeAttribute> GetDataContextChangeAttributes(ISymbol symbol)
        {
            return extractor.Extract<DataContextChangeAttribute>(symbol).ToImmutableArray();
        }

        private DataContextStackManipulationAttribute GetDataContextManipulationAttribute(ISymbol symbol)
        {
            return extractor.Extract<DataContextStackManipulationAttribute>(symbol).SingleOrDefault();
        }

        private MarkupOptionsAttribute GetMarkupOptionsAttribute(ISymbol symbol, string defaultName = null)
        {
            var attribute = extractor.Extract<MarkupOptionsAttribute>(symbol).SingleOrDefault();
            if (attribute == null && defaultName != null)
            {
                attribute = new MarkupOptionsAttribute
                {
                    AllowBinding = true,
                    AllowHardCodedValue = true,
                    MappingMode = MappingMode.Attribute,
                    Name = defaultName
                };
            }

            return attribute;
        }

        private PropertyGroupAttribute GetPropertyGroupAttribute(ISymbol symbol)
        {
            return extractor.Extract<PropertyGroupAttribute>(symbol).SingleOrDefault();
        }

        private ITypeSymbol GetStringPairValueType(ITypeSymbol collectionType)
        {
            var iEnumerable = compilation.GetTypeByMetadataName(WellKnownTypes.IEnumerable);
            var keyValuePair = compilation.GetTypeByMetadataName(WellKnownTypes.KeyValuePair);
            var @string = compilation.GetTypeByMetadataName(WellKnownTypes.String);
            return collectionType.AllInterfaces
                .Where(i =>
                {
                    // I know there are a lot of variables here, but please understand, this place is hell to debug.
                    var isIEnumerable = compilation.ClassifyConversion(i, iEnumerable).Exists;
                    var hasOneArgument = i.TypeArguments.Length == 1;
                    if (isIEnumerable && hasOneArgument && i.TypeArguments[0] is INamedTypeSymbol pairArgument)
                    {
                        var isKeyValuePair = SymbolEqualityComparer.Default
                            .Equals(pairArgument.ConstructedFrom, keyValuePair);
                        if (isKeyValuePair)
                        {
                            var hasTwoArguments = pairArgument.TypeArguments.Length == 2;
                            if (hasTwoArguments)
                            {
                                var firstIsString = SymbolEqualityComparer.Default
                                    .Equals(pairArgument.TypeArguments[0], @string);
                                return firstIsString;
                            }
                        }
                    }
                    return false;
                })
                .Select(i => (((INamedTypeSymbol)i.TypeArguments[0]).TypeArguments[1]))
                .FirstOrDefault();
        }
    }
}
