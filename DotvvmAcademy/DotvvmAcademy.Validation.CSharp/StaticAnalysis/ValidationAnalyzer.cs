using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public abstract class ValidationAnalyzer : DiagnosticAnalyzer
    {
        public static SymbolDisplayFormat CommonDisplayFormat = new SymbolDisplayFormat(
                SymbolDisplayGlobalNamespaceStyle.Omitted,
                SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                SymbolDisplayGenericsOptions.IncludeTypeParameters,
                SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeParameters,
                SymbolDisplayDelegateStyle.NameOnly,
                SymbolDisplayExtensionMethodStyle.StaticMethod,
                SymbolDisplayParameterOptions.IncludeType,
                SymbolDisplayPropertyStyle.NameOnly,
                SymbolDisplayLocalOptions.None,
                SymbolDisplayKindOptions.None,
                SymbolDisplayMiscellaneousOptions.None);

        public CSharpValidationRequest Request { get; set; }
    }
}