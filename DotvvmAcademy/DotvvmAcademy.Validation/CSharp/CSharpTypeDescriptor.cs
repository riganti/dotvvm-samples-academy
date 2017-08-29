using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class CSharpTypeDescriptor : ActivatableObject, IEquatable<CSharpTypeDescriptor>
    {
        public CSharpTypeDescriptor(ITypeSymbol symbol, bool isActive = true) : base(isActive)
        {
            if (!IsActive) return;
            Symbol = symbol;
        }

        public static CSharpTypeDescriptor Inactive { get; } = new CSharpTypeDescriptor(null, false);

        public ITypeSymbol Symbol { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is CSharpTypeDescriptor other))
            {
                return false;
            }
            return Equals(other);
        }

        public bool Equals(CSharpTypeDescriptor other)
        {
            if (!other.IsActive || !IsActive) return false;
            return Symbol.Equals(other.Symbol);
        }

        public string GetAssemblyQualifiedName()
        {
            var format = SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions & ~SymbolDisplayMiscellaneousOptions.UseSpecialTypes);
            return Symbol.ToDisplayString(format);
        }

        public string GetFriendlyName()
        {
            return Symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        }

        public override int GetHashCode()
        {
            var hashCode = -712830256;
            hashCode = hashCode * -1521134295 + EqualityComparer<ITypeSymbol>.Default.GetHashCode(Symbol);
            return hashCode;
        }
    }
}