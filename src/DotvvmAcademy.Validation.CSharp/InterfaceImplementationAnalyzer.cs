using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class InterfaceImplementationAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string NegativeMetadataKey = "IsNotImplementing";
        public const string PositiveMetadataKey = "IsImplementing";

        public static readonly DiagnosticDescriptor ForbiddenInterfaceImplementationDiagnostic = new DiagnosticDescriptor(
            id: "TEMP09",
            title: "Forbidden Interface Implementation",
            messageFormat: "The type {0} is forbidden from implementing interface {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static readonly DiagnosticDescriptor MissingInterfaceImplementationDiagnostic = new DiagnosticDescriptor(
            id: "TEMP10",
            title: "Missing Interface Implementation",
            messageFormat: "The type {0} must implement interface {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> negativeNames;
        private readonly ImmutableArray<MetadataName> positiveNames;

        public InterfaceImplementationAnalyzer(OldMetadataCollection metadata, SymbolLocator locator) : base(metadata)
        {
            positiveNames = metadata.GetNamesWithProperty(PositiveMetadataKey).ToImmutableArray();
            negativeNames = metadata.GetNamesWithProperty(NegativeMetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(ForbiddenInterfaceImplementationDiagnostic, MissingInterfaceImplementationDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction((c) => Validate(
                context: c,
                names: positiveNames,
                metadataKey: PositiveMetadataKey,
                descriptor: MissingInterfaceImplementationDiagnostic,
                reportOnContains: false));
            context.RegisterCompilationAction((c) => Validate(
                context: c,
                names: negativeNames,
                metadataKey: NegativeMetadataKey,
                descriptor: ForbiddenInterfaceImplementationDiagnostic,
                reportOnContains: true));
        }

        private void Validate(CompilationAnalysisContext context, ImmutableArray<MetadataName> names, string metadataKey,
            DiagnosticDescriptor descriptor, bool reportOnContains)
        {
            foreach (var name in names)
            {
                if (locator.TryLocate(name, out var symbol)
                    && symbol is ITypeSymbol typeSymbol)
                {
                    var interfaceNames = Metadata.RequireProperty<ImmutableArray<MetadataName>>(name, metadataKey);
                    foreach (var interfaceName in interfaceNames)
                    {
                        if (locator.TryLocate(interfaceName, out var possibleInterfaceSymbol)
                            && possibleInterfaceSymbol is INamedTypeSymbol interfaceSymbol
                            && typeSymbol.Interfaces.Contains(interfaceSymbol) == reportOnContains)
                        {
                            foreach (var location in typeSymbol.Locations)
                            {
                                context.ReportDiagnostic(Diagnostic.Create(descriptor, location, name, interfaceName));
                            }
                        }
                    }
                }
            }
        }
    }
}