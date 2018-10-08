using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class NameReflectionConverter : IMetaConverter<NameNode, MemberInfo>
    {
        private readonly IMetaContext context;

        public NameReflectionConverter(IMetaContext context)
        {
            this.context = context;
        }

        public IEnumerable<MemberInfo> Convert(NameNode source)
        {
            return Visit(source);
        }

        private IEnumerable<Type> GetTypeFromAssemblies(string fullName)
        {
            foreach (var assembly in context.Assemblies)
            {
                var type = assembly.GetType(fullName);
                if (type != null)
                {
                    yield return type;
                }
            }
        }

        private IEnumerable<MemberInfo> Visit(NameNode node)
        {
            switch (node.Kind)
            {
                case NameNodeKind.Identifier:
                case NameNodeKind.Generic:
                case NameNodeKind.Qualified:
                case NameNodeKind.NestedType:
                case NameNodeKind.PointerType:
                case NameNodeKind.ArrayType:
                    return GetTypeFromAssemblies(node.ToString());

                case NameNodeKind.ConstructedType:
                    return VisitConstructedType((ConstructedTypeNameNode)node);

                case NameNodeKind.Member:
                    return VisitMember((MemberNameNode)node);

                default:
                    throw new NotImplementedException($"{nameof(NameNodeKind)} '{node.Kind}' is not supported.");
            }
        }

        private IEnumerable<MemberInfo> VisitConstructedType(ConstructedTypeNameNode constructedType)
        {
            var arguments = constructedType.TypeArgumentList.Arguments
                .Select(a => Visit(a).Single())
                .OfType<Type>()
                .ToArray();
            return Visit(constructedType.UnboundTypeName)
                .OfType<Type>()
                .Select(t => t.MakeGenericType(arguments));
        }

        private IEnumerable<MemberInfo> VisitMember(MemberNameNode member)
        {
            var memberName = member.Member.IdentifierToken.ToString();
            return Visit(member.Type)
                .OfType<Type>()
                .SelectMany(t => t.GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic));
        }
    }
}