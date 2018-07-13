using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlMetadata : IControlResolverMetadata
    {
        private readonly ValidationTypeDescriptorFactory descriptorFactory;

        public ValidationControlMetadata(
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlType controlType,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute,
            ControlMarkupOptionsAttribute markupOptionsAttribute)
        {
            this.descriptorFactory = descriptorFactory;
            ControlType = controlType;
            DataContextChangeAttributes = changeAttributes;
            DataContextManipulationAttribute = manipulationAttribute;
            MarkupOptionsAttribute = markupOptionsAttribute;
        }

        public ValidationControlType ControlType { get; }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public ValidationTypeDescriptor DataContextConstraint => ControlType.DataContextRequirement;

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationPropertyDescriptor DefaultContentProperty
        {
            get
            {
                if (string.IsNullOrEmpty(MarkupOptionsAttribute?.DefaultContentProperty))
                {
                    return null;
                }

                return Properties.FirstOrDefault(p => p.Name == MarkupOptionsAttribute.DefaultContentProperty);
            }
        }

        public bool HasHtmlAttributesCollection
            => Type.IsAssignableTo(descriptorFactory.Create(typeof(IControlWithHtmlAttributes)));

        public ControlMarkupOptionsAttribute MarkupOptionsAttribute { get; }

        public ImmutableArray<ValidationPropertyDescriptor> Properties { get; }

        public ImmutableArray<PropertyGroupMatcher> PropertyGroupMatchers { get; }

        public ValidationTypeDescriptor Type => ControlType.Type;

        public bool IsContentAllowed
            => (MarkupOptionsAttribute?.AllowContent ?? true)
            && Type.IsAssignableTo(descriptorFactory.Create(typeof(IDotvvmControl)));

        public string VirtualPath => ControlType.VirtualPath;

        IEnumerable<IPropertyDescriptor> IControlResolverMetadata.AllProperties => Properties;

        DataContextChangeAttribute[] IControlResolverMetadata.DataContextChangeAttributes
            => DataContextChangeAttributes.ToArray();

        ITypeDescriptor IControlResolverMetadata.DataContextConstraint => DataContextConstraint;

        IPropertyDescriptor IControlResolverMetadata.DefaultContentProperty => DefaultContentProperty;

        ITypeDescriptor IControlResolverMetadata.Type => Type;

        IReadOnlyList<PropertyGroupMatcher> IControlResolverMetadata.PropertyGroups => PropertyGroupMatchers;

        IEnumerable<string> IControlResolverMetadata.PropertyNames => Properties.Select(p => p.FullName);

        bool IControlResolverMetadata.TryGetProperty(string name, out IPropertyDescriptor value)
        {
            value = Properties.FirstOrDefault(p => p.Name == name);
            return value != null;
        }
    }
}