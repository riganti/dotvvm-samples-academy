using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationPropertyDescriptorFactory
    {
        private readonly ICSharpCompilationAccessor compilationAccessor;
        private readonly ITypedAttributeExtractor extractor;

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyGroupDescriptor> groups
            = new ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyGroupDescriptor>();

        private readonly IMemberInfoConverter memberInfoConverter;

        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor> properties
            = new ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor>();

        private readonly ValidationTypeDescriptorFactory typeDescriptorFactory;

        public ValidationPropertyDescriptorFactory(
            ValidationTypeDescriptorFactory typeDescriptorFactory,
            ICSharpCompilationAccessor compilationAccessor,
            ITypedAttributeExtractor extractor,
            IMemberInfoConverter memberInfoConverter)
        {
            this.typeDescriptorFactory = typeDescriptorFactory;
            this.compilationAccessor = compilationAccessor;
            this.extractor = extractor;
            this.memberInfoConverter = memberInfoConverter;
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

            var containingType = typeDescriptorFactory.Create(dotvvmProperty.DeclaringType).TypeSymbol;
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

            var containingType = typeDescriptorFactory.Create(dotvvmGroup.DeclaringType);
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
                var attachedAttribute = extractor.ExtractRoslyn<AttachedPropertyAttribute>(fieldSymbol)
                    .SingleOrDefault();
                if (attachedAttribute == null
                    || attachedAttribute.ConstructorArguments.Length != 1
                    || attachedAttribute.ConstructorArguments[0].Kind != TypedConstantKind.Type)
                {
                    throw new ArgumentException($"'{fieldSymbol}' does not have a valid AttachedPropertyAttribute.");
                }
                var propertyType = (ITypeSymbol)attachedAttribute.ConstructorArguments[0].Value;
                return new ValidationPropertyDescriptor(
                    fieldSymbol: fieldSymbol,
                    declaringType: typeDescriptorFactory.Create(fieldSymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertyType),
                    markupOptions: extractor.ExtractMarkupOptions(fieldSymbol),
                    changeAttributes: extractor.Extract<DataContextChangeAttribute>(fieldSymbol),
                    manipulationAttribute: extractor.Extract<DataContextStackManipulationAttribute>(fieldSymbol).SingleOrDefault());
            });
        }

        public ValidationPropertyGroupDescriptor CreateCollection(IPropertySymbol propertySymbol)
        {
            return groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var groupAttribute = extractor.Extract<PropertyGroupAttribute>(propertySymbol).SingleOrDefault();
                if (groupAttribute == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' does not have " +
                        $"the {nameof(PropertyGroupAttribute)}.");
                }
                var manipulationAttribute = extractor.Extract<DataContextStackManipulationAttribute>(propertySymbol)
                    .SingleOrDefault();
                var pairValueType = GetStringPairValueType(propertySymbol.Type);
                if (pairValueType == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' is not a valid collection property group as it " +
                        $"is not of type IEnumerable<KeyValuePair<string,TValue>>.");
                }
                return new ValidationPropertyGroupDescriptor(
                    propertyFactory: this,
                    propertySymbol: propertySymbol,
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    collectionType: typeDescriptorFactory.Create(propertySymbol.Type),
                    propertyType: typeDescriptorFactory.Create(pairValueType),
                    markupOptions: extractor.ExtractMarkupOptions(propertySymbol),
                    changeAttributes: extractor.Extract<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute,
                    prefixes: groupAttribute.Prefixes.ToImmutableArray());
            });
        }

        public ValidationPropertyGroupDescriptor CreateGenerator(IPropertySymbol propertySymbol, IFieldSymbol fieldSymbol)
        {
            return groups.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var manipulationAttribute = extractor.Extract<DataContextStackManipulationAttribute>(propertySymbol)
                    .SingleOrDefault();
                // TODO: Prefix extraction from the register call
                return new ValidationPropertyGroupDescriptor(
                    propertyFactory: this,
                    propertySymbol: propertySymbol,
                    fieldSymbol: fieldSymbol,
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
                    markupOptions: extractor.ExtractMarkupOptions(propertySymbol),
                    changeAttributes: extractor.Extract<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute,
                    prefixes: default);
            });
        }

        public ValidationPropertyDescriptor CreateGrouped(ValidationPropertyGroupDescriptor propertyGroup, string groupMemberName)
        {
            var name = $"{propertyGroup.Name}{ValidationPropertyDescriptor.GroupSeparator}{groupMemberName}";
            return properties.GetOrAdd(((INamedTypeSymbol)propertyGroup.DeclaringType.TypeSymbol, name), _ =>
                new ValidationPropertyDescriptor(propertyGroup, groupMemberName));
        }

        public ImmutableArray<ValidationPropertyGroupDescriptor> CreateGroups(ITypeSymbol containingType)
        {
            if (containingType.BaseType == null)
            {
                return Enumerable.Empty<ValidationPropertyGroupDescriptor>().ToImmutableArray();
            }
            var groupSymbol = compilationAccessor.Compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmPropertyGroup);
            var builder = ImmutableArray.CreateBuilder<ValidationPropertyGroupDescriptor>();
            builder.AddRange(CreateGroups(containingType.BaseType));
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
            if (containingType.BaseType == null)
            {
                return Enumerable.Empty<ValidationPropertyDescriptor>().ToImmutableArray();
            }
            var dotvvmPropertySymbol = compilationAccessor.Compilation.GetTypeByMetadataName(DotvvmTypes.DotvvmProperty);
            var builder = ImmutableArray.CreateBuilder<ValidationPropertyDescriptor>();
            builder.AddRange(CreateProperties(containingType.BaseType));
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
                var manipulationAttribute = extractor.Extract<DataContextStackManipulationAttribute>(propertySymbol).SingleOrDefault();
                var markupOptions = extractor.Extract<MarkupOptionsAttribute>(propertySymbol).SingleOrDefault()
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
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.Extract<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        public ValidationPropertyDescriptor CreateVirtual(IPropertySymbol propertySymbol)
        {
            return properties.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var markupOptions = extractor.Extract<MarkupOptionsAttribute>(propertySymbol).SingleOrDefault();
                if (markupOptions == null)
                {
                    throw new ArgumentException($"'{propertySymbol}' does not have a MarkupOptionsAttribute.");
                }
                var manipulationAttribute = extractor.Extract<DataContextStackManipulationAttribute>(propertySymbol).SingleOrDefault();
                return new ValidationPropertyDescriptor(
                    propertySymbol: propertySymbol,
                    declaringType: typeDescriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: typeDescriptorFactory.Create(propertySymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.Extract<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        private ITypeSymbol GetStringPairValueType(ITypeSymbol collectionType)
        {
            var iEnumerable = compilationAccessor.Compilation.GetTypeByMetadataName(WellKnownTypes.IEnumerable);
            var keyValuePair = compilationAccessor.Compilation.GetTypeByMetadataName(WellKnownTypes.KeyValuePair);
            var @string = compilationAccessor.Compilation.GetTypeByMetadataName(WellKnownTypes.String);
            return collectionType.AllInterfaces
                .Where(i => compilationAccessor.Compilation.ClassifyConversion(i, iEnumerable).Exists
                    && i.TypeArguments.Length == 1
                    && compilationAccessor.Compilation.ClassifyConversion(i.TypeArguments[0], keyValuePair).Exists
                    && i.TypeArguments[0] is INamedTypeSymbol pairArgument
                    && pairArgument.TypeArguments.Length == 2
                    && pairArgument.TypeArguments[0].Equals(@string))
                .Select(i => (((INamedTypeSymbol)i.TypeArguments[0]).TypeArguments[1]))
                .FirstOrDefault();
        }
    }
}