using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("GroupDescriptor: {FullName,nq}")]
    internal class ValidationPropertyGroupDescriptor : IPropertyGroupDescriptor
    {
        public const string PropertyGroupSuffix = "GroupDescriptor";

        private readonly ValidationPropertyDescriptorFactory propertyFactory;

        /// <summary>
        /// Creates a new "collection" property group.
        /// </summary>
        public ValidationPropertyGroupDescriptor(
            ValidationPropertyDescriptorFactory propertyFactory,
            IPropertySymbol propertySymbol,
            ValidationTypeDescriptor declaringType,
            ValidationTypeDescriptor collectionType,
            ValidationTypeDescriptor propertyType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute,
            ImmutableArray<string> prefixes)
        {
            this.propertyFactory = propertyFactory;
            PropertySymbol = propertySymbol;
            DeclaringType = declaringType;
            CollectionType = collectionType;
            PropertyType = propertyType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = changeAttributes;
            DataContextManipulationAttribute = manipulationAttribute;
            Prefixes = prefixes;
        }

        /// <summary>
        /// Creates a new "generator" property group.
        /// </summary>
        public ValidationPropertyGroupDescriptor(
            ValidationPropertyDescriptorFactory propertyFactory,
            IPropertySymbol propertySymbol,
            IFieldSymbol fieldSymbol,
            ValidationTypeDescriptor declaringType,
            ValidationTypeDescriptor propertyType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute,
            ImmutableArray<string> prefixes)
        {
            this.propertyFactory = propertyFactory;
            PropertySymbol = propertySymbol;
            FieldSymbol = fieldSymbol;
            DeclaringType = declaringType;
            PropertyType = propertyType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = changeAttributes;
            DataContextManipulationAttribute = manipulationAttribute;
            Prefixes = prefixes;
        }

        public ValidationTypeDescriptor CollectionType { get; }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public IFieldSymbol FieldSymbol { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public ImmutableArray<string> Prefixes { get; }

        public IPropertySymbol PropertySymbol { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        ITypeDescriptor IPropertyGroupDescriptor.CollectionType => CollectionType;

        DataContextChangeAttribute[] IControlAttributeDescriptor.DataContextChangeAttributes
            => DataContextChangeAttributes.ToArray();

        ITypeDescriptor IControlAttributeDescriptor.DeclaringType => DeclaringType;

        string[] IPropertyGroupDescriptor.Prefixes => Prefixes.ToArray();

        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;

        public static string SanitizeName(string name)
        {
            if (name.EndsWith(PropertyGroupSuffix))
            {
                return name.Substring(0, name.Length - PropertyGroupSuffix.Length);
            }

            return name;
        }

        public ValidationPropertyDescriptor GetDotvvmProperty(string name)
        {
            return propertyFactory.CreateGrouped(this, name);
        }

        IPropertyDescriptor IPropertyGroupDescriptor.GetDotvvmProperty(string name)
            => GetDotvvmProperty(name);
    }
}