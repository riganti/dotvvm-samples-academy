using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("Control: {Metadata.Type.FullName,nq}")]
    public class ValidationControl : ValidationContentNode, IAbstractControl
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

        public ImmutableArray<ValidationPropertySetter> PropertySetters { get; private set; }

        IEnumerable<IPropertyDescriptor> IAbstractControl.PropertyNames => PropertySetters.Select(s => s.Property);

        public object[] ConstructorParameters { get; set; }

        public void AddProperty(ValidationPropertySetter property)
        {
            properties.Add(property);
            PropertySetters = properties.ToImmutable();
        }

        public bool TryGetProperty(IPropertyDescriptor property, out IAbstractPropertySetter value)
        {
            if (PropertySetters.IsDefaultOrEmpty)
            {
                value = null;
                return false;
            }
            value = PropertySetters.FirstOrDefault(s => s.Property.Equals(property));
            return value != null;
        }
    }
}