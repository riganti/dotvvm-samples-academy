using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Meta.Syntax
{
    public class ArrayTypeNameNode : NameNode, IEquatable<ArrayTypeNameNode>
    {
        public ArrayTypeNameNode(
            NameNode elementType,
            NameToken openBracketToken,
            NameToken closeBracketToken,
            ImmutableArray<NameToken> commaTokens)
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

        public static bool operator !=(ArrayTypeNameNode node1, ArrayTypeNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(ArrayTypeNameNode node1, ArrayTypeNameNode node2)
        {
            return EqualityComparer<ArrayTypeNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ArrayTypeNameNode);
        }

        public bool Equals(ArrayTypeNameNode other)
        {
            return other != null &&
                EqualityComparer<NameToken>.Default.Equals(CloseBracketToken, other.CloseBracketToken) &&
                CommaTokens.Equals(other.CommaTokens) &&
                EqualityComparer<NameNode>.Default.Equals(ElementType, other.ElementType) &&
                EqualityComparer<NameToken>.Default.Equals(OpenBracketToken, other.OpenBracketToken);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CloseBracketToken, CommaTokens, ElementType, OpenBracketToken);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(ElementType);
            sb.Append('[');
            for (int i = 0; i < CommaTokens.Length; i++)
            {
                sb.Append(',');
            }
            sb.Append(']');
            return sb.ToString();
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitArrayType(this);
        }
    }
}
