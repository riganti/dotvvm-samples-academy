using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class IdentifierNameNode : SimpleNameNode, IEquatable<IdentifierNameNode>
    {
        public IdentifierNameNode(NameToken identifierToken, ImmutableArray<NameDiagnostic> diagnostics = default)
            : base(NameNodeKind.Identifier, diagnostics)
        {
            IdentifierToken = identifierToken;
        }

        public override NameToken IdentifierToken { get; }

        public static bool operator !=(IdentifierNameNode node1, IdentifierNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(IdentifierNameNode node1, IdentifierNameNode node2)
        {
            return EqualityComparer<IdentifierNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IdentifierNameNode);
        }

        public bool Equals(IdentifierNameNode other)
        {
            return other != null &&
                   EqualityComparer<NameToken>.Default.Equals(IdentifierToken, other.IdentifierToken);
        }

        public override int GetHashCode()
        {
            return 736718691 + EqualityComparer<NameToken>.Default.GetHashCode(IdentifierToken);
        }

        public override NameNode SetDiagnostics(ImmutableArray<NameDiagnostic> diagnostics)
        {
            return new IdentifierNameNode(IdentifierToken, diagnostics);
        }

        public override string ToString()
        {
            return IdentifierToken.ToString();
        }
    }
}