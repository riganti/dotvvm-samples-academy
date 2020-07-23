using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("TreeRoot")]
    public class ValidationTreeRoot : ValidationContentNode, IAbstractTreeRoot
    {
        private readonly Dictionary<string, List<IAbstractDirective>> directivesDictionary;

        public ValidationTreeRoot(
            DothtmlRootNode node,
            ValidationControlMetadata metadata,
            IDataContextStack dataContext,
            ImmutableArray<ValidationDirective> directives)
            : base(node, metadata, dataContext)
        {
            Directives = directives;
            directivesDictionary = directives.GroupBy(d => d.Name).ToDictionary(g => g.Key, g => g.Cast<IAbstractDirective>().ToList());
        }

        public ImmutableArray<ValidationDirective> Directives { get; }

        Dictionary<string, List<IAbstractDirective>> IAbstractTreeRoot.Directives => directivesDictionary;

        public string FileName { get; set; }

        protected override ValidationTreeRoot GetTreeRoot()
        {
            return this;
        }
    }
}
