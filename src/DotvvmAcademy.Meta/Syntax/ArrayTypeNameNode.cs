using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class ArrayTypeNameNode : NameNode
    {
        public ArrayTypeNameNode(
            NameNode elementType,
            NameToken openBracketToken,
            NameToken closeBracketToken,
            ImmutableArray<NameToken> commaTokens,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.ArrayType, diagnostics)
        {
            ElementType = elementType;
            OpenBracketToken = openBracketToken;
            CloseBracketToken = closeBracketToken;
            CommaTokens = commaTokens;
        }

        public NameToken CloseBracketToken { get; }

        public ImmutableArray<NameToken> CommaTokens { get; }

        public NameNode ElementType { get; }

        public NameToken OpenBracketToken { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new ArrayTypeNameNode(ElementType, OpenBracketToken, CloseBracketToken, CommaTokens, diagnostics);
        }
    }
}