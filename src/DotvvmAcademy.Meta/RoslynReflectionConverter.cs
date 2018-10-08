using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class RoslynReflectionConverter : IMetaConverter<ISymbol, MemberInfo>
    {
        private readonly IMetaConverter<NameNode, MemberInfo> nameConverter;
        private readonly IMetaConverter<ISymbol, NameNode> symbolConverter;

        public RoslynReflectionConverter(
            IMetaConverter<ISymbol, NameNode> symbolConverter,
            IMetaConverter<NameNode, MemberInfo> nameConverter)
        {
            this.symbolConverter = symbolConverter;
            this.nameConverter = nameConverter;
        }

        public IEnumerable<MemberInfo> Convert(ISymbol source)
        {
            // TODO: Implement without conversion to NameNode
            return symbolConverter.Convert(source).SelectMany(nameConverter.Convert);
        }
    }
}