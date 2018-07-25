using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class IdentifierNameNode : SimpleNameNode
    {
        public IdentifierNameNode(NameToken identifierToken, ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Identifier, diagnostics)
        {
            IdentifierToken = identifierToken;
        }

        public override NameToken IdentifierToken { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new IdentifierNameNode(IdentifierToken, diagnostics);
        }
    }
}