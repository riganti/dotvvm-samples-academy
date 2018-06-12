using DotvvmAcademy.Validation.CSharp.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeKindAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "TypeKind";

        public static readonly DiagnosticDescriptor WrongTypeKindDiagnostic = new DiagnosticDescriptor(
            id: "TEMP07",
            title: "Wrong Type Kind",
            messageFormat: "The kind of type {0} must be one of these: {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public TypeKindAnalyzer(MetadataCollection<MetadataName> metadata, SymbolLocator locator) : base(metadata)
        {
            names = GetNamesWithProperty(MetadataKey);
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(WrongTypeKindDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            foreach (var name in names)
            {
                var desired = Metadata.GetRequiredProperty<CSharpTypeKind>(name, MetadataKey);
                if(locator.TryLocate(name, out var symbol)
                    && symbol is ITypeSymbol typeSymbol
                    && (desired & ConvertTypeKind(typeSymbol.TypeKind)) == 0)
                {
                    foreach(var location in typeSymbol.Locations)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(WrongTypeKindDiagnostic, location, name, desired.ToString("G")));
                    }
                }
            }
        }

        private CSharpTypeKind ConvertTypeKind(TypeKind kind)
        {
            switch(kind)
            {
                case TypeKind.Class:
                    return CSharpTypeKind.Class;
                case TypeKind.Delegate:
                    return CSharpTypeKind.Delegate;
                case TypeKind.Enum:
                    return CSharpTypeKind.Enum;
                case TypeKind.Interface:
                    return CSharpTypeKind.Interface;
                case TypeKind.Struct:
                    return CSharpTypeKind.Struct;
                default:
                    return CSharpTypeKind.None;
            }
        }
    }
}