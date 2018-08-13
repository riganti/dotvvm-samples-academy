using Microsoft.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class SymbolConverter : ISymbolConverter
    {
        private readonly IMemberInfoLocator locator;
        private readonly ISymbolNameBuilder nameBuilder;

        public SymbolConverter(ISymbolNameBuilder nameBuilder, IMemberInfoLocator locator)
        {
            this.nameBuilder = nameBuilder;
            this.locator = locator;
        }

        public MemberInfo Convert(ISymbol symbol)
        {
            // TODO: Property ISymbol -> MemberInfo conversion
            return locator.Locate(nameBuilder.Build(symbol)).FirstOrDefault();
        }
    }
}