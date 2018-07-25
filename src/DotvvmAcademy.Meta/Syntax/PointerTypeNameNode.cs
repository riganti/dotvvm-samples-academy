using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class PointerTypeNameNode : NameNode
    {
        public PointerTypeNameNode(
            NameNode elementType,
            NameToken asteriskToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.PointerType, diagnostics)
        {
            ElementType = elementType;
            AsteriskToken = asteriskToken;
        }

        public NameToken AsteriskToken { get; }

        public NameNode ElementType { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new PointerTypeNameNode(ElementType, AsteriskToken, diagnostics);
        }
    }
}