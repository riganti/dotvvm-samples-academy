using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyControlCollection: {Property.FullName,nq}, Count = {Controls.Length,nq}")]
    public class ValidationPropertyControlCollection : ValidationPropertySetter, IAbstractPropertyControlCollection
    {
        public ValidationPropertyControlCollection(
            DothtmlNode node,
            IPropertyDescriptor property,
            ImmutableArray<ValidationControl> controls)
            : base(node, property)
        {
            Controls = controls;
            foreach (var control in Controls)
            {
                control.Parent = this;
            }
        }

        public ImmutableArray<ValidationControl> Controls { get; set; }

        IEnumerable<IAbstractControl> IAbstractPropertyControlCollection.Controls => Controls;
    }
}