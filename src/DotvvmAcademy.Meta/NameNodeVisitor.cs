using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    internal class NameNodeVisitor<TResult>
    {
        public virtual TResult Visit(NameNode node)
        {
            return node.Accept(this);
        }

        public virtual TResult DefaultVisit(NameNode node)
        {
            return default;
        }

        public virtual TResult VisitArrayType(ArrayTypeNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitConstructedType(ConstructedTypeNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitGeneric(GenericNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitIdentifier(IdentifierNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitMember(MemberNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitNestedType(NestedTypeNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitPointerType(PointerTypeNameNode node)
        {
            return DefaultVisit(node);
        }

        public virtual TResult VisitQualified(QualifiedNameNode node)
        {
            return DefaultVisit(node);
        }
    }
}
