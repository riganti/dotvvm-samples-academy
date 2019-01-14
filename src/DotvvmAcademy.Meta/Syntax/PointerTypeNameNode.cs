using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class PointerTypeNameNode : NameNode
    {
        public PointerTypeNameNode(
            NameNode elementType,
            NameToken asteriskToken)
        {
            ElementType = elementType;
            AsteriskToken = asteriskToken;
        }

        public NameToken AsteriskToken { get; }

        public NameNode ElementType { get; }

        public override string ToString()
        {
            return $"{ElementType}*";
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitPointerType(this);
        }
    }
}