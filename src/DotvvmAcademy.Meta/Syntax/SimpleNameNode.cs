using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public abstract class SimpleNameNode : NameNode
    {
        public SimpleNameNode(NameNodeKind kind, ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(kind, diagnostics)
        {
        }

        public abstract NameToken IdentifierToken { get; }
    }
}