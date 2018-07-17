using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyTemplate: {Property.FullName,nq}, Count = {Content.Length,nq}")]
    internal class ValidationPropertyTemplate : ValidationPropertySetter, IAbstractPropertyTemplate
    {
        public ValidationPropertyTemplate(
            DothtmlNode node,
            ValidationPropertyDescriptor property,
            ImmutableArray<ValidationControl> content)
            : base(node, property)
        {
            Content = content;
        }

        public ImmutableArray<ValidationControl> Content { get; }

        IEnumerable<IAbstractControl> IAbstractPropertyTemplate.Content => Content;
    }
}