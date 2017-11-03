using DotvvmAcademy.Validation.CSharp.Resources;
using Microsoft.CodeAnalysis;
using System;
using System.Resources;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public static class DiagnosticDescriptors
    {
        private static ResourceManager manager = DiagnosticResources.ResourceManager;
        private static Type resourcesType = typeof(DiagnosticResources);

        public static readonly DiagnosticDescriptor MissingMember = new DiagnosticDescriptor(
            ValidationDiagnosticIds.MissingMember,
            new LocalizableResourceString(nameof(DiagnosticResources.MissingMemberTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.MissingMemberMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);

        public static readonly DiagnosticDescriptor RedundantMember = new DiagnosticDescriptor(
            ValidationDiagnosticIds.RedundantMember,
            new LocalizableResourceString(nameof(DiagnosticResources.RedundantMemberTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.RedundantMemberMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);

        public static readonly DiagnosticDescriptor IncorrectAccessModifier = new DiagnosticDescriptor(
            ValidationDiagnosticIds.IncorrectAccessModifier,
            new LocalizableResourceString(nameof(DiagnosticResources.IncorrectAccessModifierTitle), manager, resourcesType),
            new LocalizableResourceString(nameof(DiagnosticResources.IncorrectAccessModifierMessage), manager, resourcesType),
            DiagnosticCategories.ValidationErrors,
            DiagnosticSeverity.Error,
            true);
    }
}