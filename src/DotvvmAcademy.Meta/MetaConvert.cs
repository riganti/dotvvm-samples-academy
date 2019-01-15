using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
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

        public static (int line, int column) ToCoords(string source, int index)
        {
            if (index == -1 || index >= source.Length)
            {
                return (-1, -1);
            }

            var line = 1;
            var column = 0;

            for (int i = 0; i < index; ++i)
            {
                ++column;
                if (source[i] == '\n')
                {
                    column = 0;
                    ++line;
                }
            }
            return (line, column + 1);
        }

        public static int ToIndex(string source, int line, int column)
        {
            if ((line, column) == (-1, -1))
            {
                return -1;
            }

            int index;
            for (index = 0; index < source.Length; ++index)
            {
                if (source[index] == '\n')
                {
                    --line;
                    if (line == 1)
                    {
                        break;
                    }
                }
            }
            if (line != 1)
            {
                // string doesn't have enough lines
                return -1;
            }

            return index + column;
        }
    }
}