using Microsoft.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MemberInfoConverter : IMemberInfoConverter
    {
        private readonly ISymbolLocator locator;
        private readonly IMemberInfoNameBuilder nameBuilder;

        public MemberInfoConverter(IMemberInfoNameBuilder nameBuilder, ISymbolLocator locator)
        {
            this.nameBuilder = nameBuilder;
            this.locator = locator;
        }

        public ISymbol Convert(MemberInfo memberInfo)
        {
            // TODO: Proper MemberInfo -> ISymbol conversion
            return locator.Locate(nameBuilder.Build(memberInfo)).SingleOrDefault();
        }
    }
}