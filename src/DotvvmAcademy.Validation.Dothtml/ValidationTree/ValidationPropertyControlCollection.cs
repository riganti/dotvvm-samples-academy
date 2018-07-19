using DotVVM.Framework.Binding;
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
            ValidationPropertyDescriptor property,
            ImmutableArray<ValidationControl> controls)
            : base(node, property)
        {
            Controls = controls;
        }

        public ImmutableArray<ValidationControl> Controls { get; set; }

        IEnumerable<IAbstractControl> IAbstractPropertyControlCollection.Controls => Controls;
    }
}