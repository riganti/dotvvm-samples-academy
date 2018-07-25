using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class GenericNameNode : SimpleNameNode
    {
        public GenericNameNode(
            NameToken identifierToken,
            NameToken backtickToken,
            NameToken arityToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Generic, diagnostics)
        {
            IdentifierToken = identifierToken;
            BacktickToken = backtickToken;
            ArityToken = arityToken;
        }

        public NameToken ArityToken { get; }

        public NameToken BacktickToken { get; }

        public override NameToken IdentifierToken { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new GenericNameNode(IdentifierToken, BacktickToken, ArityToken, diagnostics);
        }
    }
}