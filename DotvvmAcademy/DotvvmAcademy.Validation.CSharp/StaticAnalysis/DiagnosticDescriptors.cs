using DotvvmAcademy.Validation.CSharp.Resources;
using Microsoft.CodeAnalysis;
using System;
using System.Resources;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public static class DiagnosticDescriptors
    {
        private static ResourceManager manager = DiagnosticResources.ResourceManager;
        private static Type resourcesType = typeof(DiagnosticResources);

        public static readonly DiagnosticDescriptor MissingSymbol = new DiagnosticDescriptor(
            DiagnosticIds.MissingSymbol,
            new LocalizableResourceString(nameof(DiagnosticResources.MissingSymbolTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.MissingSymbolMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);

        public static readonly DiagnosticDescriptor RedundantSymbol = new DiagnosticDescriptor(
            DiagnosticIds.RedundantSymbol,
            new LocalizableResourceString(nameof(DiagnosticResources.RedundantSymbolTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.RedundantSymbolMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);

        public static readonly DiagnosticDescriptor IncorrectAccessModifier = new DiagnosticDescriptor(
            DiagnosticIds.IncorrectAccessModifier,
            new LocalizableResourceString(nameof(DiagnosticResources.IncorrectAccessModifierTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.IncorrectAccessModifierMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);

        public static readonly DiagnosticDescriptor DisallowedSymbol = new DiagnosticDescriptor(
            DiagnosticIds.DisallowedSymbol,
            new LocalizableResourceString(nameof(DiagnosticResources.DisallowedSymbolTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.DisallowedSymbolMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);
    }
}