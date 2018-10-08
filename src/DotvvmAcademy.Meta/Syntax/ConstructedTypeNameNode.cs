using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class ConstructedTypeNameNode : NameNode, IEquatable<ConstructedTypeNameNode>
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

        public static bool operator !=(ConstructedTypeNameNode node1, ConstructedTypeNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(ConstructedTypeNameNode node1, ConstructedTypeNameNode node2)
        {
            return EqualityComparer<ConstructedTypeNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConstructedTypeNameNode);
        }

        public bool Equals(ConstructedTypeNameNode other)
        {
            return other != null &&
                   EqualityComparer<TypeArgumentListNode>.Default.Equals(TypeArgumentList, other.TypeArgumentList) &&
                   EqualityComparer<NameNode>.Default.Equals(UnboundTypeName, other.UnboundTypeName);
        }

        public override int GetHashCode()
        {
            var hashCode = -251295557;
            hashCode = hashCode * -1521134295 + EqualityComparer<TypeArgumentListNode>.Default.GetHashCode(TypeArgumentList);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameNode>.Default.GetHashCode(UnboundTypeName);
            return hashCode;
        }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new ConstructedTypeNameNode(UnboundTypeName, TypeArgumentList, diagnostics);
        }

        public override string ToString()
        {
            return $"{UnboundTypeName}{TypeArgumentList}";
        }
    }
}