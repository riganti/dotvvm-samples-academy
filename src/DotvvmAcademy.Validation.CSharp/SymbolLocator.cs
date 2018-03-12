using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolLocator : ILocator<ISymbol>
    {
        private readonly Compilation compilation;
        private readonly RoslynMetadataNameProvider nameProvider;

        public SymbolLocator(Compilation compilation, RoslynMetadataNameProvider nameProvider)
        {
            this.compilation = compilation;
            this.nameProvider = nameProvider;
        }

        public bool TryLocate(MetadataName name, out ISymbol symbol)
        {
            try
            {
                symbol = name.Kind.HasFlag(MetadataNameKind.Type)
                    ? GetNamedTypeSymbol(name)
                    : GetMemberSymbol(name);
            }
            catch (ArgumentException)
            {
                symbol = null;
            }
            return symbol != null;
        }

        private ISymbol GetMemberSymbol(MetadataName name)
        {
            var containingType = GetNamedTypeSymbol(name.Owner);
            var members = containingType?.GetMembers(name.Name) ?? ImmutableArray.Create<ISymbol>();
            return members.FirstOrDefault(m => nameProvider.GetName(m).Equals(name));
        }

        private INamedTypeSymbol GetNamedTypeSymbol(MetadataName name)
        {
            return compilation.GetTypeByMetadataName(name.ReflectionName);
        }
    }
}