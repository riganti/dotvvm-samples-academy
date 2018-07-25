using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public abstract class NameNode
    {
        public NameNode(NameNodeKind kind, ImmutableArray<NameDiagnostic> diagnostics = default)
        {
            Kind = kind;
            Diagnostics = diagnostics;
        }

        public ImmutableArray<NameDiagnostic> Diagnostics { get; }

        public NameNodeKind Kind { get; }

        public abstract NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics);
    }
}