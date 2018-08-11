using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public static class RoslynPresets
    {
        public static readonly ImmutableArray<SymbolKind> AllSymbols = ImmutableArray.Create(
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

        public static readonly ImmutableArray<SyntaxKind> Declarations = ImmutableArray.Create(
            SyntaxKind.NamespaceDeclaration,
            SyntaxKind.ClassDeclaration,
            SyntaxKind.StructDeclaration,
            SyntaxKind.InterfaceDeclaration,
            SyntaxKind.EnumDeclaration,
            SyntaxKind.DelegateDeclaration,
            SyntaxKind.EnumMemberDeclaration,
            SyntaxKind.FieldDeclaration,
            SyntaxKind.EventFieldDeclaration,
            SyntaxKind.MethodDeclaration,
            SyntaxKind.OperatorDeclaration,
            SyntaxKind.ConversionOperatorDeclaration,
            SyntaxKind.ConstructorDeclaration,
            SyntaxKind.DestructorDeclaration,
            SyntaxKind.PropertyDeclaration,
            SyntaxKind.EventDeclaration,
            SyntaxKind.IndexerDeclaration,
            SyntaxKind.GetAccessorDeclaration,
            SyntaxKind.SetAccessorDeclaration,
            SyntaxKind.AddAccessorDeclaration,
            SyntaxKind.RemoveAccessorDeclaration,
            SyntaxKind.UnknownAccessorDeclaration);

        public static readonly ImmutableArray<SyntaxKind> Identifiers = ImmutableArray.Create(
            SyntaxKind.PredefinedType,
            SyntaxKind.IdentifierName);
    }
}