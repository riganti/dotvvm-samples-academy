using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    internal class RoslynMemberInfoVisitor : MemberInfoVisitor<IEnumerable<ISymbol>>
    {
        private readonly Compilation compilation;

        public RoslynMemberInfoVisitor(Compilation compilation)
        {
            this.compilation = compilation;
        }

        public override IEnumerable<ISymbol> DefaultVisit(MemberInfo info)
        {
            throw new NotSupportedException($"MemberInfo of type \"{info.GetType()}\" is not supported.");
        }

        public override IEnumerable<ISymbol> VisitConstructor(ConstructorInfo info)
        {
            var parameters = info.GetParameters()
                .Select(p => Visit(p.ParameterType).OfType<ITypeSymbol>().First());

            return Visit(info.DeclaringType)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(info.Name))
                .OfType<IMethodSymbol>()
                .Where(m =>
                {
                    return m.Parameters.Select(p => p.Type)
                        .SequenceEqual(parameters);
                });
        }

        public override IEnumerable<ISymbol> VisitEvent(EventInfo info)
        {
            return Visit(info.DeclaringType)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(info.Name))
                .OfType<IEventSymbol>();
        }

        public override IEnumerable<ISymbol> VisitField(FieldInfo info)
        {
            return Visit(info.DeclaringType)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(info.Name))
                .OfType<IFieldSymbol>();
        }

        public override IEnumerable<ISymbol> VisitMethod(MethodInfo info)
        {
            var parameters = info.GetParameters()
                .Select(p => Visit(p.ParameterType).OfType<ITypeSymbol>().First());

            return Visit(info.DeclaringType)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(info.Name))
                .OfType<IMethodSymbol>()
                .Where(m =>
                {
                    return m.Parameters.Select(p => p.Type)
                        .SequenceEqual(parameters);
                });
        }

        public override IEnumerable<ISymbol> VisitProperty(PropertyInfo info)
        {
            return Visit(info.DeclaringType)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(info.Name))
                .OfType<IPropertySymbol>();
        }

        public override IEnumerable<ISymbol> VisitType(Type info)
        {
            if (info.IsConstructedGenericType)
            {
                var typeArguments = info.GenericTypeArguments
                    .Select(a => Visit(a).OfType<ITypeSymbol>().First())
                    .ToArray();

                return Visit(info.GetGenericTypeDefinition())
                    .OfType<INamedTypeSymbol>()
                    .Select(t => t.Construct(typeArguments));
            }
            else if (info.IsPointer)
            {
                return Visit(info)
                    .OfType<ITypeSymbol>()
                    .Select(t => compilation.CreatePointerTypeSymbol(t));
            }
            else if (info.IsArray)
            {
                return Visit(info)
                    .OfType<ITypeSymbol>()
                    .Select(t => compilation.CreateArrayTypeSymbol(t, info.GetArrayRank()));
            }
            else
            {
                return new[] { compilation.GetTypeByMetadataName(info.FullName) };
            }
        }
    }
}
