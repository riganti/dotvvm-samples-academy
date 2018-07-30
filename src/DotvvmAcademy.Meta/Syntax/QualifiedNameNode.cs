using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class QualifiedNameNode : NameNode
    {
        public QualifiedNameNode(
            NameNode left,
            SimpleNameNode right,
            NameToken dotToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Qualified, diagnostics)
        {
            Left = left;
            Right = right;
            DotToken = dotToken;
        }

        public NameToken DotToken { get; }

        public NameNode Left { get; }

        public SimpleNameNode Right { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new QualifiedNameNode(Left, Right, DotToken, diagnostics);
        }

        public override string ToString()
        {
            return $"{Left}.{Right}";
        }
    }
}