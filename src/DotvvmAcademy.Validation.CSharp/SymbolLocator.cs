using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolLocator
    {
        private readonly Compilation compilation;
        private readonly RoslynMetadataNameProvider nameProvider;

        public SymbolLocator(Compilation compilation, RoslynMetadataNameProvider nameProvider)
        {
            this.compilation = compilation;
            this.nameProvider = nameProvider;
        }

        public bool TryGetSymbol(MetadataName name, out ISymbol symbol)
        {
            try
            {
                symbol = name.IsType
                    ? GetNamedTypeSymbol(name)
                    : GetMemberSymbol(name);
            }
            catch(ArgumentException)
            {
                symbol = null;
            }
                return symbol != null;
        }

        private ISymbol GetMemberSymbol(MetadataName name)
        {
            var containingType = GetNamedTypeSymbol(name.Owner);
            var members = containingType?.GetMembers(name.Name) ?? ImmutableArray.Create<ISymbol>();
            return members.First(m => nameProvider.GetName(m).Equals(name));
        }

        private INamedTypeSymbol GetNamedTypeSymbol(MetadataName name)
        {
            return compilation.GetTypeByMetadataName(name.ReflectionName);
        }
    }
}