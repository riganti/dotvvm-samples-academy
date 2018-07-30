using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ContentNode: {Metadata.Type.FullName,nq}")]
    public abstract class ValidationContentNode : ValidationTreeNode, IAbstractContentNode
    {
        private ImmutableArray<ValidationControl>.Builder content = ImmutableArray.CreateBuilder<ValidationControl>();

        public ValidationContentNode(
            DothtmlNode node,
            ValidationControlMetadata metadata,
            IDataContextStack dataContext)
            : base(node)
        {
            Metadata = metadata;
            DataContextTypeStack = dataContext;
        }

        public ImmutableArray<ValidationControl> Content { get; private set;}

        IEnumerable<IAbstractControl> IAbstractContentNode.Content => Content;

        public IDataContextStack DataContextTypeStack { get; set; }

        public ValidationControlMetadata Metadata { get; }

        IControlResolverMetadata IAbstractContentNode.Metadata => Metadata;

        public void AddChildControl(ValidationControl child)
        {
            content.Add(child);
            Content = content.ToImmutable();
        }
    }
}