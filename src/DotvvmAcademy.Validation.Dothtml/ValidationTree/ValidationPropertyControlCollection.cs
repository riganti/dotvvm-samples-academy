using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyControlCollection : ValidationPropertySetter, IAbstractPropertyControlCollection
    {
        public ValidationPropertyControlCollection(
            DothtmlNode node,
            DotvvmProperty property,
            ImmutableArray<ValidationControl> controls)
            : base(node, property)
        {
            Controls = controls;
        }

        public ImmutableArray<ValidationControl> Controls { get; set; }

        IEnumerable<IAbstractControl> IAbstractPropertyControlCollection.Controls => Controls;
    }
}