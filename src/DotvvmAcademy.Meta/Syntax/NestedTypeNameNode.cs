using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class NestedTypeNameNode : NameNode
    {
        public NestedTypeNameNode(
            NameNode left,
            SimpleNameNode right,
            NameToken plusToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.NestedType, diagnostics)
        {
            Left = left;
            Right = right;
            PlusToken = plusToken;
        }

        public NameNode Left { get; }

        public NameToken PlusToken { get; }

        public SimpleNameNode Right { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new NestedTypeNameNode(Left, Right, PlusToken, diagnostics);
        }
    }
}