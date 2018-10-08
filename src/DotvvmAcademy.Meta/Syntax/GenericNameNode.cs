using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class GenericNameNode : SimpleNameNode, IEquatable<GenericNameNode>
    {
        public GenericNameNode(
            NameToken identifierToken,
            NameToken backtickToken,
            NameToken arityToken,
            ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Generic, diagnostics)
        {
            IdentifierToken = identifierToken;
            BacktickToken = backtickToken;
            ArityToken = arityToken;
        }

        public NameToken ArityToken { get; }

        public NameToken BacktickToken { get; }

        public override NameToken IdentifierToken { get; }

        public static bool operator !=(GenericNameNode node1, GenericNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(GenericNameNode node1, GenericNameNode node2)
        {
            return EqualityComparer<GenericNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GenericNameNode);
        }

        public bool Equals(GenericNameNode other)
        {
            return other != null &&
                   EqualityComparer<NameToken>.Default.Equals(ArityToken, other.ArityToken) &&
                   EqualityComparer<NameToken>.Default.Equals(BacktickToken, other.BacktickToken) &&
                   EqualityComparer<NameToken>.Default.Equals(IdentifierToken, other.IdentifierToken);
        }

        public override int GetHashCode()
        {
            var hashCode = 695767046;
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(ArityToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(BacktickToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(IdentifierToken);
            return hashCode;
        }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new GenericNameNode(IdentifierToken, BacktickToken, ArityToken, diagnostics);
        }

        public override string ToString()
        {
            return $"{IdentifierToken}`{ArityToken}";
        }
    }
}