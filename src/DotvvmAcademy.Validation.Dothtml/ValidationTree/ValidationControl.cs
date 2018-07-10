using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControl : ValidationContentNode, IAbstractControl
    {
        public ValidationControl(
            DothtmlNode node,
            ImmutableArray<ValidationControl> content,
            ValidationControlMetadata metadata,
            ImmutableArray<ValidationPropertySetter> properties)
            : base(node, content, metadata)
        {
            Properties = properties;
        }

        public ImmutableArray<ValidationPropertySetter> Properties { get; set; }

        IEnumerable<IPropertyDescriptor> IAbstractControl.PropertyNames => Properties.Select(s => s.Property);

        object[] IAbstractControl.ConstructorParameters { get; set; }

        public bool TryGetProperty(IPropertyDescriptor property, out IAbstractPropertySetter value)
        {
            value = Properties.SingleOrDefault(s => s.Property.Equals(property));
            return value != null;
        }
    }
}