using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class ReflectionRoslynConverter : IMetaConverter<MemberInfo, ISymbol>
    {
        private readonly IMetaConverter<MemberInfo, NameNode> memberInfoConverter;
        private readonly IMetaConverter<NameNode, ISymbol> nameConverter;

        public ReflectionRoslynConverter(
            IMetaConverter<MemberInfo, NameNode> memberInfoConverter,
            IMetaConverter<NameNode, ISymbol> nameConverter)
        {
            this.memberInfoConverter = memberInfoConverter;
            this.nameConverter = nameConverter;
        }

        public IEnumerable<ISymbol> Convert(MemberInfo source)
        {
            // TODO: Implement without conversion to NameNode
            return memberInfoConverter.Convert(source).SelectMany(nameConverter.Convert);
        }
    }
}