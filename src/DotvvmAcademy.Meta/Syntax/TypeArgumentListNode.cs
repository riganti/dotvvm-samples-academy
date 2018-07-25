using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class TypeArgumentListNode : NameNode
    {
        public TypeArgumentListNode(
            ImmutableArray<NameNode> arguments,
            ImmutableArray<NameToken> commaTokens,
            NameToken openBracketToken,
            NameToken closeBracketToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.TypeArgumentList, diagnostics)
        {
            Arguments = arguments;
            CommaTokens = commaTokens;
            OpenBracketToken = openBracketToken;
            CloseBracketToken = closeBracketToken;
        }

        public ImmutableArray<NameNode> Arguments { get; }

        public NameToken CloseBracketToken { get; }

        public ImmutableArray<NameToken> CommaTokens { get; }

        public NameToken OpenBracketToken { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new TypeArgumentListNode(Arguments, CommaTokens, OpenBracketToken, CloseBracketToken, diagnostics);
        }
    }
}