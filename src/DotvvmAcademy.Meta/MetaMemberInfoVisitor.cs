using DotvvmAcademy.Meta.Syntax;
using System;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    internal class MetaMemberInfoVisitor : MemberInfoVisitor<NameNode>
    {
        public override NameNode DefaultVisit(MemberInfo info)
        {
            throw new NotSupportedException($"Symbol of type \"{info.GetType()}\" is not supported.");
        }

        public override NameNode VisitConstructor(ConstructorInfo info)
        {
            return VisitMember(info);
        }

        public override NameNode VisitEvent(EventInfo info)
        {
            return VisitMember(info);
        }

        public override NameNode VisitField(FieldInfo info)
        {
            return VisitMember(info);
        }

        public override NameNode VisitMethod(MethodInfo info)
        {
            return VisitMember(info);
        }

        public override NameNode VisitProperty(PropertyInfo info)
        {
            return VisitMember(info);
        }

        public override NameNode VisitType(Type info)
        {
            if (info.IsConstructedGenericType)
            {
                var arguments = info.GetGenericArguments()
                    .Select(a => Visit(a));
                return NameFactory.ConstructedType(Visit(info.GetGenericTypeDefinition()), arguments);
            }
            else if (info.IsNested)
            {
                return NameFactory.NestedType(Visit(info.DeclaringType), info.Name, info.GetGenericArguments().Length);
            }
            else if (info.IsPointer)
            {
                return NameFactory.PointerType(Visit(info.GetElementType()));
            }
            else if (info.IsArray)
            {
                return NameFactory.ArrayType(Visit(info.GetElementType()), info.GetArrayRank());
            }
            else
            {
                if (info.Namespace == null)
                {
                    return NameFactory.Simple(info.Name, info.GenericTypeArguments.Length);
                }
                else
                {
                    return NameFactory.Qualified(VisitNamespace(info.Namespace), info.Name, info.GenericTypeArguments.Length);
                }
            }
        }

        private NameNode VisitNamespace(string @namespace)
        {
            var segments = @namespace.Split('.');
            NameNode result = NameFactory.Identifier(segments[0]);
            for (int i = 1; i < segments.Length; i++)
            {
                result = NameFactory.Qualified(result, segments[i]);
            }
            return result;
        }

        private NameNode VisitMember(MemberInfo info)
        {
            return NameFactory.Member(Visit(info.DeclaringType), info.Name);
        }
    }
}