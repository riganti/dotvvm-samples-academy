using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Meta.Syntax
{
    public class MemberNameNode : NameNode, IEquatable<MemberNameNode>
    {
        public MemberNameNode(
            NameNode type,
            IdentifierNameNode member,
            NameToken colonColonToken)
        {
            Type = type;
            Member = member;
            ColonColonToken = colonColonToken;
        }

        public NameToken ColonColonToken { get; }

        public IdentifierNameNode Member { get; }

        public NameNode Type { get; }

        public static bool operator !=(MemberNameNode node1, MemberNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(MemberNameNode node1, MemberNameNode node2)
        {
            return EqualityComparer<MemberNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MemberNameNode);
        }

        public bool Equals(MemberNameNode other)
        {
            return other != null &&
                   EqualityComparer<NameToken>.Default.Equals(ColonColonToken, other.ColonColonToken) &&
                   EqualityComparer<IdentifierNameNode>.Default.Equals(Member, other.Member) &&
                   EqualityComparer<NameNode>.Default.Equals(Type, other.Type);
        }

        public override int GetHashCode()
        {
            var hashCode = -1967349164;
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(ColonColonToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<IdentifierNameNode>.Default.GetHashCode(Member);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameNode>.Default.GetHashCode(Type);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Type}::{Member}";
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitMember(this);
        }
    }
}