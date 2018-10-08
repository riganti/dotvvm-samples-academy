using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Meta.Syntax
{
    // TODO: Merge with ConstructedTypeNameNode
    public class TypeArgumentListNode : NameNode, IEquatable<TypeArgumentListNode>
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

        public static bool operator !=(TypeArgumentListNode node1, TypeArgumentListNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(TypeArgumentListNode node1, TypeArgumentListNode node2)
        {
            return EqualityComparer<TypeArgumentListNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeArgumentListNode);
        }

        public bool Equals(TypeArgumentListNode other)
        {
            return other != null &&
                   Arguments.Equals(other.Arguments) &&
                   CloseBracketToken.Equals(other.CloseBracketToken) &&
                   CommaTokens.Equals(other.CommaTokens) &&
                   OpenBracketToken.Equals(other.OpenBracketToken);
        }

        public override int GetHashCode()
        {
            var hashCode = -2126204417;
            hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableArray<NameNode>>.Default.GetHashCode(Arguments);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(CloseBracketToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableArray<NameToken>>.Default.GetHashCode(CommaTokens);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(OpenBracketToken);
            return hashCode;
        }

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