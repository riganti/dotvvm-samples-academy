using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class NestedTypeNameNode : NameNode, IEquatable<NestedTypeNameNode>
    {
        public NestedTypeNameNode(
            NameNode left,
            SimpleNameNode right,
            NameToken plusToken)
        {
            Left = left;
            Right = right;
            PlusToken = plusToken;
        }

        public NameNode Left { get; }

        public NameToken PlusToken { get; }

        public SimpleNameNode Right { get; }

        public static bool operator !=(NestedTypeNameNode node1, NestedTypeNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(NestedTypeNameNode node1, NestedTypeNameNode node2)
        {
            return EqualityComparer<NestedTypeNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NestedTypeNameNode);
        }

        public bool Equals(NestedTypeNameNode other)
        {
            return other != null &&
                   EqualityComparer<NameNode>.Default.Equals(Left, other.Left) &&
                   PlusToken.Equals(other.PlusToken) &&
                   EqualityComparer<SimpleNameNode>.Default.Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            var hashCode = -1235852409;
            hashCode = hashCode * -1521134295 + EqualityComparer<NameNode>.Default.GetHashCode(Left);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(PlusToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<SimpleNameNode>.Default.GetHashCode(Right);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Left}+{Right}";
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitNestedType(this);
        }
    }
}