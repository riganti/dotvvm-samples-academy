using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class PredefinedTypeNameNode : SimpleNameNode
    {
        public PredefinedTypeNameNode(
            NameNodeKind kind,
            NameToken identifierToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(kind, diagnostics)
        {
            IdentifierToken = identifierToken;
        }

        public override NameToken IdentifierToken { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new PredefinedTypeNameNode(Kind, IdentifierToken, diagnostics);
        }
    }
}