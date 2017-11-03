using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum CSharpValidationExtent
    {
        AllowedSymbolsAnalyzer = 1,
        AssemblyRewrite = 2,
        ExplicitStaticAnalysis = 4,
        ExplicitDynamicAnalysis = 8,
        ValidationMethods = ExplicitStaticAnalysis | ExplicitDynamicAnalysis,
        General = AllowedSymbolsAnalyzer | AssemblyRewrite,
        All = General | ValidationMethods
    }
}