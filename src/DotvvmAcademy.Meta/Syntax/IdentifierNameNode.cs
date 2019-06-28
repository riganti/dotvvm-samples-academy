using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class IdentifierNameNode : SimpleNameNode, IEquatable<IdentifierNameNode>
    {
        public IdentifierNameNode(NameToken identifierToken)
            : base(identifierToken)
        {
        }

        public static bool operator !=(IdentifierNameNode node1, IdentifierNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(IdentifierNameNode node1, IdentifierNameNode node2)
        {
            return EqualityComparer<IdentifierNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IdentifierNameNode);
        }

        public bool Equals(IdentifierNameNode other)
        {
            return other != null &&
                   EqualityComparer<NameToken>.Default.Equals(IdentifierToken, other.IdentifierToken);
        }

        public override int GetHashCode()
        {
            return 736718691 + EqualityComparer<NameToken>.Default.GetHashCode(IdentifierToken);
        }

        public override string ToString()
        {
            return IdentifierToken.ToString();
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitIdentifier(this);
        }
    }
}