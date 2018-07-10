﻿using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTreeRoot : ValidationContentNode, IAbstractTreeRoot
    {
        private Dictionary<string, List<IAbstractDirective>> directivesDictionary;

        public ValidationTreeRoot(
            DothtmlRootNode node,
            ImmutableArray<ValidationControl> content,
            ValidationControlMetadata metadata,
            ImmutableArray<ValidationDirective> directives)
            : base(node, content, metadata)
        {
            Directives = directives;
            directivesDictionary = directives.GroupBy(d => d.Name).ToDictionary(g => g.Key, g => g.Cast<IAbstractDirective>().ToList());
        }

        public ImmutableArray<ValidationDirective> Directives { get; }

        Dictionary<string, List<IAbstractDirective>> IAbstractTreeRoot.Directives => directivesDictionary;

        public string FileName { get; set; }
    }
}