using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("Control: {Metadata.Type.FullName,nq}")]
    public class ValidationControl : ValidationContentNode, IAbstractControl
    {
        public ValidationControl(
            DothtmlNode node,
            ValidationControlMetadata metadata,
            IDataContextStack dataContext)
            : base(node, metadata, dataContext)
        {
        }

        public object[] ConstructorParameters { get; set; }

        public ImmutableArray<ValidationPropertySetter> PropertySetters { get; private set; }
            = ImmutableArray.Create<ValidationPropertySetter>();

        IEnumerable<IPropertyDescriptor> IAbstractControl.PropertyNames => PropertySetters.Select(s => s.Property);

        public void AddProperty(ValidationPropertySetter property)
        {
            property.Parent = this;
            PropertySetters = PropertySetters.Add(property);
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