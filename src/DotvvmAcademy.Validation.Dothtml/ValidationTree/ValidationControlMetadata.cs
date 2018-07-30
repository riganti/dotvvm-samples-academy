using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ControlMetadata: {Type.FullName,nq}")]
    public class ValidationControlMetadata : IControlResolverMetadata
    {
        public ValidationControlMetadata(
            ValidationControlType controlType,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute,
            ControlMarkupOptionsAttribute markupOptionsAttribute,
            ImmutableArray<ValidationPropertyDescriptor> properties,
            ImmutableArray<PropertyGroupMatcher> propertyGroupMatchers)
        {
            ControlType = controlType;
            DataContextChangeAttributes = changeAttributes;
            DataContextManipulationAttribute = manipulationAttribute;
            MarkupOptionsAttribute = markupOptionsAttribute;
            Properties = properties;
            PropertyGroupMatchers = propertyGroupMatchers;
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

        public bool HasHtmlAttributesCollection => Type.IsAssignableTo(typeof(IControlWithHtmlAttributes));

        public bool IsContentAllowed
            => (MarkupOptionsAttribute?.AllowContent ?? true)
            && Type.IsAssignableTo(typeof(IDotvvmControl));

        public ControlMarkupOptionsAttribute MarkupOptionsAttribute { get; }

        public ImmutableArray<ValidationPropertyDescriptor> Properties { get; }

        public ImmutableArray<PropertyGroupMatcher> PropertyGroupMatchers { get; }

        IEnumerable<IPropertyDescriptor> IControlResolverMetadata.AllProperties => Properties;

        DataContextChangeAttribute[] IControlResolverMetadata.DataContextChangeAttributes
        {
            get
            {
                if (DataContextChangeAttributes.IsDefaultOrEmpty)
                {
                    return Array.Empty<DataContextChangeAttribute>();
                }

                return DataContextChangeAttributes.ToArray();
            }
        }

        ITypeDescriptor IControlResolverMetadata.DataContextConstraint => DataContextConstraint;

        IPropertyDescriptor IControlResolverMetadata.DefaultContentProperty => DefaultContentProperty;

        IReadOnlyList<PropertyGroupMatcher> IControlResolverMetadata.PropertyGroups => PropertyGroupMatchers;

        IEnumerable<string> IControlResolverMetadata.PropertyNames => Properties.Select(p => p.FullName);

        public ValidationTypeDescriptor Type => ControlType.Type;

        ITypeDescriptor IControlResolverMetadata.Type => Type;

        public string VirtualPath => ControlType.VirtualPath;

        bool IControlResolverMetadata.TryGetProperty(string name, out IPropertyDescriptor value)
        {
            value = Properties.FirstOrDefault(p => p.Name == name);
            return value != null;
        }
    }
}