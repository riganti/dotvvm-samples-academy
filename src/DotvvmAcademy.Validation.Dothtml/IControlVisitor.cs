using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public interface IControlVisitor
    {
        IMetadataCollection<DothtmlIdentifier> Metadata { set; }

        ImmutableArray<ValidationDiagnostic> GetDiagnostics();

        void Visit(DothtmlIdentifier identifier, ResolvedControl control);
    }
}