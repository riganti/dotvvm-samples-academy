using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class MetaConvert
    {
        public static NameNode ToMeta(ISymbol symbol)
        {
            var visitor = new MetaSymbolVisitor();
            return visitor.Visit(symbol);
        }

        public static NameNode ToMeta(MemberInfo info)
        {
            var visitor = new MetaMemberInfoVisitor();
            return visitor.Visit(info);
        }

        public static IEnumerable<MemberInfo> ToReflection(NameNode node, IEnumerable<Assembly> assemblies)
        {
            var visitor = new ReflectionNameNodeVisitor(assemblies);
            return visitor.Visit(node);
        }

        public static IEnumerable<MemberInfo> ToReflection(ISymbol symbol, IEnumerable<Assembly> assemblies)
        {
            var visitor = new ReflectionSymbolVisitor(assemblies);
            return visitor.Visit(symbol);
        }

        public static IEnumerable<ISymbol> ToRoslyn(NameNode node, Compilation compilation)
        {
            var visitor = new RoslynNameNodeVisitor(compilation);
            return visitor.Visit(node);
        }

        public static IEnumerable<ISymbol> ToRoslyn(MemberInfo info, Compilation compilation)
        {
            var visitor = new RoslynMemberInfoVisitor(compilation);
            return visitor.Visit(info);
        }
    }
}