using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ViewModuleDirective: {Type,nq}")]
    public class ValidationViewModuleDirective : ValidationDirective, IAbstractViewModuleDirective
    {
        public ValidationViewModuleDirective(
            DothtmlDirectiveNode node,
            string importedModule,
            string importedResourceName)
            : base(node)
        {
            ImportedModule = importedModule;
            ImportedResourceName = importedResourceName;
        }

        public string ImportedModule { get; }

        public string ImportedResourceName { get; }
    }
}
