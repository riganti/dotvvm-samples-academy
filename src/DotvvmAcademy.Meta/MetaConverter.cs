using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MetaConverter
    {
        private readonly MetaSymbolVisitor metaSymbolVisitor;
        private readonly MetaMemberInfoVisitor metaMemberInfoVisitor;
        private readonly RoslynMemberInfoVisitor roslynMemberInfoVisitor;
        private readonly RoslynNameNodeVisitor roslynNameNodeVisitor;
        private readonly ReflectionSymbolVisitor reflectionSymbolVisitor;
        private readonly ReflectionNameNodeVisitor reflectionNameNodeVisitor;

        public MetaConverter(Compilation compilation, IEnumerable<Assembly> assemblies)
        {
            metaSymbolVisitor = new MetaSymbolVisitor();
            metaMemberInfoVisitor = new MetaMemberInfoVisitor();
            roslynMemberInfoVisitor = new RoslynMemberInfoVisitor(compilation);
            roslynNameNodeVisitor = new RoslynNameNodeVisitor(compilation);
            reflectionSymbolVisitor = new ReflectionSymbolVisitor(assemblies);
            reflectionNameNodeVisitor = new ReflectionNameNodeVisitor(assemblies);
        }

        public NameNode ToMeta(ISymbol symbol)
        {
            return metaSymbolVisitor.Visit(symbol);
        }

        public NameNode ToMeta(MemberInfo info)
        {
            return metaMemberInfoVisitor.Visit(info);
        }

        public IEnumerable<MemberInfo> ToReflection(NameNode node)
        {
            return reflectionNameNodeVisitor.Visit(node);
        }

        public IEnumerable<MemberInfo> ToReflection(ISymbol symbol)
        {
            return reflectionSymbolVisitor.Visit(symbol);
        }

        public IEnumerable<ISymbol> ToRoslyn(NameNode node)
        {
            return roslynNameNodeVisitor.Visit(node);
        }

        public IEnumerable<ISymbol> ToRoslyn(MemberInfo info)
        {
            return roslynMemberInfoVisitor.Visit(info);
        }
    }
}