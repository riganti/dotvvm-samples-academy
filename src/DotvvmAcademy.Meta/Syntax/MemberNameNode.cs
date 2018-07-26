using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class MemberNameNode : NameNode
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

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new MemberNameNode(Type, Member, ColonColonToken, diagnostics);
        }

        public override string ToString()
        {
            return $"{Type}::{Member}";
        }
    }
}