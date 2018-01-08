using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum CSharpValidationExtent
    {
        None = 0,
        StaticAnalysis = 1 << 0,
        DynamicAnalysis = 1 << 1,
        AssemblyRewrite = 1 << 2,
        All = StaticAnalysis | DynamicAnalysis | AssemblyRewrite
    }
}