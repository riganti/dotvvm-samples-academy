using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControl : ValidationContentNode, IAbstractControl
    {
        private ImmutableArray<ValidationPropertySetter>.Builder properties
            = ImmutableArray.CreateBuilder<ValidationPropertySetter>();

        public ValidationControl(
            DothtmlNode node,
            ValidationControlMetadata metadata,
            IDataContextStack dataContext)
            : base(node, metadata, dataContext)
        {
        }

        public ImmutableArray<ValidationPropertySetter> Properties { get; private set; }

        IEnumerable<IPropertyDescriptor> IAbstractControl.PropertyNames => Properties.Select(s => s.Property);

        object[] IAbstractControl.ConstructorParameters { get; set; }

        public void AddProperty(ValidationPropertySetter property)
        {
            properties.Add(property);
            Properties = properties.ToImmutable();
        }

        public bool TryGetProperty(IPropertyDescriptor property, out IAbstractPropertySetter value)
        {
            value = Properties.SingleOrDefault(s => s.Property.Equals(property));
            return value != null;
        }
    }
}