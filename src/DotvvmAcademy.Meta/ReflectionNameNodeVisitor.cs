using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    internal class ReflectionNameNodeVisitor : NameNodeVisitor<IEnumerable<MemberInfo>>
    {
        private readonly ImmutableArray<Assembly> assemblies;

        public ReflectionNameNodeVisitor(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies.ToImmutableArray();
        }

        public override IEnumerable<MemberInfo> DefaultVisit(NameNode node)
        {
            return GetTypes(node);
        }

        public override IEnumerable<MemberInfo> VisitConstructedType(ConstructedTypeNameNode node)
        {
            var arguments = node.Arguments
                .Select(a => GetTypes(a).First())
                .OfType<Type>()
                .ToArray();
            return GetTypes(node.UnboundTypeName)
                .Select(t => t.MakeGenericType(arguments));
        }

        public override IEnumerable<MemberInfo> VisitMember(MemberNameNode node)
        {
            return GetTypes(node.Type)
                .SelectMany(t => t.GetMember(node.Member.IdentifierToken.ToString(), BindingFlags.Public | BindingFlags.NonPublic));
        }

        private IEnumerable<Type> GetTypes(NameNode node)
        {
            return assemblies.GetTypes(node.ToString());
        }
    }
}