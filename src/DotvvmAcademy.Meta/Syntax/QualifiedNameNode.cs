using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class QualifiedNameNode : NameNode, IEquatable<QualifiedNameNode>
    {
        public QualifiedNameNode(
            NameNode left,
            SimpleNameNode right,
            NameToken dotToken)
        {
            Left = left;
            Right = right;
            DotToken = dotToken;
        }

        public NameToken DotToken { get; }

        public NameNode Left { get; }

        public SimpleNameNode Right { get; }

        public static bool operator !=(QualifiedNameNode node1, QualifiedNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(QualifiedNameNode node1, QualifiedNameNode node2)
        {
            return EqualityComparer<QualifiedNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as QualifiedNameNode);
        }

        public bool Equals(QualifiedNameNode other)
        {
            return other != null &&
                   DotToken.Equals(other.DotToken) &&
                   EqualityComparer<NameNode>.Default.Equals(Left, other.Left) &&
                   EqualityComparer<SimpleNameNode>.Default.Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            var hashCode = -181700454;
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(DotToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameNode>.Default.GetHashCode(Left);
            hashCode = hashCode * -1521134295 + EqualityComparer<SimpleNameNode>.Default.GetHashCode(Right);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Left}.{Right}";
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitQualified(this);
        }
    }
}