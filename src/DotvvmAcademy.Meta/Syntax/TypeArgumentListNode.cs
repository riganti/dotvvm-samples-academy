using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Meta.Syntax
{
    // TODO: Merge with ConstructedTypeNameNode
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            for (int i = 0; i < Arguments.Length; i++)
            {
                sb.Append(Arguments[i]);
                if (i < Arguments.Length - 1)
                {
                    sb.Append(',');
                }
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}