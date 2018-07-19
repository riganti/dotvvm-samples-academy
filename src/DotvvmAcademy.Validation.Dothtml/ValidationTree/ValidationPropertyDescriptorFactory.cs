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
    public class ValidationPropertyDescriptorFactory
    {
        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly AttributeExtractor extractor;

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyGroupDescriptor> groups
            = new ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyGroupDescriptor>();

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor> properties
            = new ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor>();

        public ValidationPropertyDescriptorFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory,
            AttributeExtractor extractor)
        {
            this.compilation = compilation;
            this.descriptorFactory = descriptorFactory;
            this.extractor = extractor;
        }

        public ValidationPropertyDescriptor Convert(IPropertyDescriptor property)
        {
            if (property is ValidationPropertyDescriptor propertyDescriptor)
            {
                return propertyDescriptor;
            }

            if (property is GroupedDotvvmProperty grouped)
            {
                return CreateGrouped(Convert(grouped.PropertyGroup), grouped.GroupMemberName);
            }

            if (!(property is DotvvmProperty dotvvmProperty))
            {
                throw new ArgumentException($"Passed IPropertyDescriptor is of unknown type: '{property.GetType()}'.");
            }

            var containingType = descriptorFactory.Create(dotvvmProperty.DeclaringType).TypeSymbol;
            var propertySymbol = containingType
                .GetMembers(property.Name)
                .OfType<IPropertySymbol>()
                .SingleOrDefault();
            var fieldSymbol = containingType
                .GetMembers($"{property.Name}{ValidationPropertyDescriptor.PropertySuffix}")
                .OfType<IFieldSymbol>()
                .SingleOrDefault();
            if (propertySymbol != null && fieldSymbol != null)
            {
                return CreateRegular(propertySymbol, fieldSymbol);
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
        }

        public ValidationPropertyGroupDescriptor Convert(IPropertyGroupDescriptor group)
        {
            if (group is ValidationPropertyGroupDescriptor groupDescriptor)
            {
                return groupDescriptor;
            }

            if (!(group is DotvvmPropertyGroup dotvvmGroup))
            {
                throw new ArgumentException($"Passed IPropertyGroupDescriptor is of " +
                    $"unknown type: '{group.GetType()}'.");
            }

            var containingType = descriptorFactory.Create(dotvvmGroup.DeclaringType);
            var generatorCandidate = containingType.TypeSymbol
                .GetMembers($"{group.Name}{ValidationPropertyGroupDescriptor.PropertyGroupSuffix}")
                .OfType<IFieldSymbol>()
                .SingleOrDefault();
            var collectionCandidate = containingType.TypeSymbol
                .GetMembers(group.Name)
                .OfType<IPropertySymbol>()
                .SingleOrDefault();
            if (generatorCandidate != null && collectionCandidate != null)
            {
                return CreateGenerator(collectionCandidate, generatorCandidate);
            }

            if (collectionCandidate != null)
            {
                return CreateCollection(collectionCandidate);
            }

            throw new ArgumentException($"DotvvmPropertyGroup '{group}' could not be converted.");
        }

        public ValidationPropertyDescriptor CreateAttached(IFieldSymbol fieldSymbol)
        {
            var name = ValidationPropertyDescriptor.SanitizeName(fieldSymbol.MetadataName);
            return properties.GetOrAdd((fieldSymbol.ContainingType, name), _ =>
            {
                var attachedAttribute = extractor.GetFirstAttributeData<AttachedPropertyAttribute>(fieldSymbol);
                if (attachedAttribute == null
                    || attachedAttribute.ConstructorArguments.Length != 1
                    || attachedAttribute.ConstructorArguments[0].Kind != TypedConstantKind.Type)
                {
                    throw new ArgumentException($"'{fieldSymbol}' does not have a valid AttachedPropertyAttribute.");
                }
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(fieldSymbol)
                    ?? new MarkupOptionsAttribute();
                var propertyType = (ITypeSymbol)attachedAttribute.ConstructorArguments[0].Value;
                return new ValidationPropertyDescriptor(
                    fieldSymbol: fieldSymbol,
                    declaringType: descriptorFactory.Create(fieldSymbol.ContainingType),
                    propertyType: descriptorFactory.Create(propertyType),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(fieldSymbol),
                    manipulationAttribute: extractor.GetAttribute<DataContextStackManipulationAttribute>(fieldSymbol));
            });
        }

        public ValidationPropertyGroupDescriptor CreateCollection(IPropertySymbol propertySymbol)
        {
            return groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var groupAttribute = extractor.GetAttribute<PropertyGroupAttribute>(propertySymbol);
                if (groupAttribute == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' does not have " +
                        $"the {nameof(PropertyGroupAttribute)}.");
                }
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol)
                    ?? new MarkupOptionsAttribute();
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                var pairValueType = GetStringPairValueType(propertySymbol.Type);
                if (pairValueType == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' is not a valid collection property group as it " +
                        $"is not of type IEnumerable<KeyValuePair<string,TValue>>.");
                }
                return new ValidationPropertyGroupDescriptor(
                    propertyFactory: this,
                    propertySymbol: propertySymbol,
                    declaringType: descriptorFactory.Create(propertySymbol.ContainingType),
                    collectionType: descriptorFactory.Create(propertySymbol.Type),
                    propertyType: descriptorFactory.Create(pairValueType),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute,
                    prefixes: groupAttribute.Prefixes.ToImmutableArray());
            });
        }

        public ValidationPropertyGroupDescriptor CreateGenerator(
            IPropertySymbol propertySymbol,
            IFieldSymbol fieldSymbol)
        {
            return groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol)
                    ?? new MarkupOptionsAttribute();
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                // TODO: Prefix extraction from the register call
                return new ValidationPropertyGroupDescriptor(
                    propertyFactory: this,
                    propertySymbol: propertySymbol,
                    fieldSymbol: fieldSymbol,
                    declaringType: descriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: descriptorFactory.Create(propertySymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute,
                    prefixes: default(ImmutableArray<string>));
            });
        }

        public ValidationPropertyDescriptor CreateGrouped(
            ValidationPropertyGroupDescriptor propertyGroup,
            string groupMemberName)
        {
            var name = $"{propertyGroup.Name}{ValidationPropertyDescriptor.GroupSeparator}{groupMemberName}";
            return properties.GetOrAdd(((INamedTypeSymbol)propertyGroup.DeclaringType.TypeSymbol, name), _ =>
                new ValidationPropertyDescriptor(propertyGroup, groupMemberName));
        }

        public ImmutableArray<ValidationPropertyGroupDescriptor> CreateGroups(ITypeSymbol containingType)
        {
            var groupSymbol = compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmPropertyGroup);
            var builder = ImmutableArray.CreateBuilder<ValidationPropertyGroupDescriptor>();
            var collectionGroups = containingType.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => extractor.HasAttribute<PropertyGroupAttribute>(p)
                    && GetStringPairValueType(p.Type) != null);
            foreach (var group in collectionGroups)
            {
                builder.Add(CreateCollection(group));
            }
            var generatorGroups = containingType.GetMembers()
                .OfType<IFieldSymbol>()
                .Where(f => f.IsStatic
                    && f.Type.Equals(groupSymbol));
            foreach (var group in generatorGroups)
            {
                var propertySymbol = containingType
                    .GetMembers(ValidationPropertyGroupDescriptor.SanitizeName(group.MetadataName))
                    .OfType<IPropertySymbol>()
                    .SingleOrDefault();
                if (propertySymbol != null)
                {
                    builder.Add(CreateGenerator(propertySymbol, group));
                }
            }
            return builder.ToImmutable();
        }

        public ImmutableArray<ValidationPropertyDescriptor> CreateProperties(ITypeSymbol containingType)
        {
            var dotvvmPropertySymbol = compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmProperty);
            var builder = ImmutableArray.CreateBuilder<ValidationPropertyDescriptor>();
            var fields = containingType.GetMembers()
                .OfType<IFieldSymbol>()
                .Where(f => f.IsStatic
                    && f.MetadataName.EndsWith(ValidationPropertyDescriptor.PropertySuffix)
                    && f.Type.Equals(dotvvmPropertySymbol));
            foreach (var field in fields)
            {
                if (extractor.HasAttribute<AttachedPropertyAttribute>(field))
                {
                    builder.Add(CreateAttached(field));
                }
                else
                {
                    var propertyName = ValidationPropertyDescriptor.SanitizeName(field.MetadataName);
                    var propertyCounterpart = containingType.GetMembers(propertyName)
                        .OfType<IPropertySymbol>()
                        .SingleOrDefault();
                    if (propertyCounterpart == null)
                    {
                        continue;
                    }

                    builder.Add(CreateRegular(propertyCounterpart, field));
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

        public ValidationPropertyDescriptor CreateRegular(IPropertySymbol propertySymbol, IFieldSymbol fieldSymbol)
        {
            return properties.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol)
                    ?? new MarkupOptionsAttribute
                    {
                        AllowBinding = true,
                        AllowHardCodedValue = true,
                        MappingMode = MappingMode.Attribute,
                        Name = propertySymbol.MetadataName
                    };
                return new ValidationPropertyDescriptor(
                    propertySymbol: propertySymbol,
                    fieldSymbol: fieldSymbol,
                    declaringType: descriptorFactory.Create(fieldSymbol.ContainingType),
                    propertyType: descriptorFactory.Create(fieldSymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        public ValidationPropertyDescriptor CreateVirtual(IPropertySymbol propertySymbol)
        {
            return properties.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol);
                if (markupOptions == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' does not have a MarkupOptionsAttribute.");
                }
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                return new ValidationPropertyDescriptor(
                    propertySymbol: propertySymbol,
                    declaringType: descriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: descriptorFactory.Create(propertySymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        private ITypeSymbol GetStringPairValueType(ITypeSymbol collectionType)
        {
            var iEnumerable = compilation.GetTypeByMetadataName(WellKnownTypes.IEnumerable);
            var keyValuePair = compilation.GetTypeByMetadataName(WellKnownTypes.KeyValuePair);
            var @string = compilation.GetTypeByMetadataName(WellKnownTypes.String);
            return collectionType.AllInterfaces
                .Where(i => compilation.ClassifyConversion(i, iEnumerable).Exists
                    && i.TypeArguments.Length == 1
                    && compilation.ClassifyConversion(i.TypeArguments[0], keyValuePair).Exists
                    && i.TypeArguments[0] is INamedTypeSymbol pairArgument
                    && pairArgument.TypeArguments.Length == 2
                    && pairArgument.TypeArguments[0].Equals(@string))
                .Select(i => (((INamedTypeSymbol)i.TypeArguments[0]).TypeArguments[1]))
                .FirstOrDefault();
        }
    }
}