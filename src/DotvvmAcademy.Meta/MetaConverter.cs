using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MetaConverter
    {
        public NameNode ToMeta(ISymbol symbol)
        {
            var visitor = new MetaSymbolVisitor();
            return visitor.Visit(symbol);
        }

        public NameNode ToMeta(MemberInfo info)
        {
            var visitor = new MetaMemberInfoVisitor();
            return visitor.Visit(info);
        }

        public IEnumerable<MemberInfo> ToReflection(NameNode node, IEnumerable<Assembly> assemblies)
        {
            var visitor = new ReflectionNameNodeVisitor(assemblies);
            return visitor.Visit(node);
        }

        public IEnumerable<MemberInfo> ToReflection(ISymbol symbol, IEnumerable<Assembly> assemblies)
        {
            var visitor = new ReflectionSymbolVisitor(assemblies);
            return visitor.Visit(symbol);
        }

        public IEnumerable<ISymbol> ToRoslyn(NameNode node, Compilation compilation)
        {
            var visitor = new RoslynNameNodeVisitor(compilation);
            return visitor.Visit(node);
        }

        public IEnumerable<ISymbol> ToRoslyn(MemberInfo info, Compilation compilation)
        {
            var visitor = new RoslynMemberInfoVisitor(compilation);
            return visitor.Visit(info);
        }
    }
}