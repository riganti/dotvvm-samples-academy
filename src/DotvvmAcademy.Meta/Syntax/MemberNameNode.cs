using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class MemberNameNode : NameNode, IEquatable<MemberNameNode>
    {
        public MemberNameNode(
            NameNode type,
            IdentifierNameNode member,
            NameToken colonColonToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Member, diagnostics)
        {
            Type = type;
            Member = member;
            ColonColonToken = colonColonToken;
        }

        public NameToken ColonColonToken { get; }

        public IdentifierNameNode Member { get; }

        public NameNode Type { get; }

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

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new MemberNameNode(Type, Member, ColonColonToken, diagnostics);
        }

        public override string ToString()
        {
            return $"{Type}::{Member}";
        }

        public static bool operator ==(MemberNameNode node1, MemberNameNode node2)
        {
            return EqualityComparer<MemberNameNode>.Default.Equals(node1, node2);
        }

        public static bool operator !=(MemberNameNode node1, MemberNameNode node2)
        {
            return !(node1 == node2);
        }
    }
}