using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class ConstructedTypeNameNode : NameNode
    {
        public ConstructedTypeNameNode(
            NameNode unboundTypeName,
            TypeArgumentListNode typeArgumentList,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.ConstructedType, diagnostics)
        {
            UnboundTypeName = unboundTypeName;
            TypeArgumentList = typeArgumentList;
        }

        public TypeArgumentListNode TypeArgumentList { get; }

        public NameNode UnboundTypeName { get; }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new ConstructedTypeNameNode(UnboundTypeName, TypeArgumentList, diagnostics);
        }
    }
}