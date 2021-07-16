#nullable enable

using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("TreeRoot")]
    public class ValidationTreeRoot : ValidationControl, IAbstractTreeRoot
    {
        public const string DefaultFileName = "Unnamed";
        private readonly Dictionary<string, List<IAbstractDirective>> directivesDictionary;

        public ValidationTreeRoot(
            DothtmlRootNode node,
            ValidationControlMetadata metadata,
            IDataContextStack dataContext,
            ImmutableArray<ValidationDirective> directives,
            IAbstractControlBuilderDescriptor? masterPage)
            : base(node, metadata, dataContext)
        {
            Directives = directives;
            MasterPage = masterPage;
            directivesDictionary = directives.GroupBy(d => d.Name).ToDictionary(g => g.Key, g => g.Cast<IAbstractDirective>().ToList());
        }

        public ImmutableArray<ValidationDirective> Directives { get; }

        Dictionary<string, List<IAbstractDirective>> IAbstractTreeRoot.Directives => directivesDictionary;

        public string? FileName { get; set; } = DefaultFileName;

        public IAbstractControlBuilderDescriptor? MasterPage { get; }

        protected override ValidationTreeRoot GetTreeRoot()
        {
            return this;
        }
    }
}
