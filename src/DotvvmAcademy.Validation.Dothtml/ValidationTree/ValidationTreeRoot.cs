using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTreeRoot : ValidationContentNode, IAbstractTreeRoot
    {
        public ValidationTreeRoot(
            DothtmlRootNode node,
            ImmutableArray<ValidationControl> content,
            ValidationControlResolverMetadata metadata,
            ImmutableArray<ValidationDirective> directives)
            : base(node, content, metadata)
        {
            Directives = directives;
        }

        public ImmutableArray<ValidationDirective> Directives { get; }

        Dictionary<string, List<IAbstractDirective>> IAbstractTreeRoot.Directives { get; } = null;

        public string FileName { get; set; }
    }
}