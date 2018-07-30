using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MemberInfoLocator : ILocator<MemberInfo>
    {
        private readonly ImmutableArray<Assembly> assemblies;

        public MemberInfoLocator(ImmutableArray<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public ImmutableArray<MemberInfo> Locate(string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return Locate(parser.Parse());
        }

        public ImmutableArray<MemberInfo> Locate(NameNode node)
        {
            return Visit(node).ToImmutableArray();
        }

        private IEnumerable<Type> GetType(string fullName)
        {
            foreach (var assembly in assemblies)
            {
                yield return assembly.GetType(fullName);
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
                    return GetType(node.ToString());

                case NameNodeKind.BoolType:
                case NameNodeKind.ByteType:
                case NameNodeKind.SByteType:
                case NameNodeKind.IntType:
                case NameNodeKind.UIntType:
                case NameNodeKind.ShortType:
                case NameNodeKind.UShortType:
                case NameNodeKind.LongType:
                case NameNodeKind.ULongType:
                case NameNodeKind.FloatType:
                case NameNodeKind.DoubleType:
                case NameNodeKind.DecimalType:
                case NameNodeKind.StringType:
                case NameNodeKind.CharType:
                case NameNodeKind.ObjectType:
                case NameNodeKind.VoidType:
                    return VisitPredefinedType((PredefinedTypeNameNode)node);

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

        private IEnumerable<MemberInfo> VisitPredefinedType(PredefinedTypeNameNode predefinedType)
        {
            MemberInfo result;
            switch (predefinedType.Kind)
            {
                case NameNodeKind.BoolType:
                    result = typeof(bool);
                    break;

                case NameNodeKind.ByteType:
                    result = typeof(byte);
                    break;

                case NameNodeKind.SByteType:
                    result = typeof(sbyte);
                    break;

                case NameNodeKind.IntType:
                    result = typeof(int);
                    break;

                case NameNodeKind.UIntType:
                    result = typeof(uint);
                    break;

                case NameNodeKind.ShortType:
                    result = typeof(short);
                    break;

                case NameNodeKind.UShortType:
                    result = typeof(ushort);
                    break;

                case NameNodeKind.LongType:
                    result = typeof(long);
                    break;

                case NameNodeKind.ULongType:
                    result = typeof(ulong);
                    break;

                case NameNodeKind.FloatType:
                    result = typeof(float);
                    break;

                case NameNodeKind.DoubleType:
                    result = typeof(double);
                    break;

                case NameNodeKind.DecimalType:
                    result = typeof(decimal);
                    break;

                case NameNodeKind.StringType:
                    result = typeof(string);
                    break;

                case NameNodeKind.CharType:
                    result = typeof(char);
                    break;

                case NameNodeKind.ObjectType:
                    result = typeof(object);
                    break;

                case NameNodeKind.VoidType:
                    result = typeof(void);
                    break;

                default:
                    throw new NotSupportedException($"{nameof(NameNodeKind)} '{predefinedType.Kind}' is not " +
                        $"a supported prefined type kind.");
            }
            return ImmutableArray.Create(result);
        }
    }
}