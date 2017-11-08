using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public static class SymbolKindPresets
    {
        public static ImmutableArray<SymbolKind> All = ImmutableArray.Create(
            SymbolKind.Alias,
            SymbolKind.ArrayType,
            SymbolKind.Assembly,
            SymbolKind.DynamicType,
            SymbolKind.ErrorType,
            SymbolKind.Event,
            SymbolKind.Field,
            SymbolKind.Label,
            SymbolKind.Local,
            SymbolKind.Method,
            SymbolKind.NetModule,
            SymbolKind.NamedType,
            SymbolKind.Namespace,
            SymbolKind.Parameter,
            SymbolKind.PointerType,
            SymbolKind.Property,
            SymbolKind.RangeVariable,
            SymbolKind.TypeParameter,
            SymbolKind.Preprocessing,
            SymbolKind.Discard);
    }
}