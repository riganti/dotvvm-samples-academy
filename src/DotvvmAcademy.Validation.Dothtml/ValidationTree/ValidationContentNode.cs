using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal abstract class ValidationContentNode : ValidationTreeNode, IAbstractContentNode
    {
        public ValidationContentNode(
            DothtmlNode node,
            ImmutableArray<ValidationControl> content,
            ValidationControlResolverMetadata metadata)
            : base(node)
        {
            Content = content;
            Metadata = metadata;
        }

        public ImmutableArray<ValidationControl> Content { get; }

        IEnumerable<IAbstractControl> IAbstractContentNode.Content => Content;

        public IDataContextStack DataContextTypeStack { get; set; }

        public ValidationControlResolverMetadata Metadata { get; }

        IControlResolverMetadata IAbstractContentNode.Metadata => Metadata;
    }
}