using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum CSharpValidationExtent
    {
        None = 0b000,
        StaticAnalysis = 0b001,
        DynamicAnalysis = 0b010,
        AssemblyRewrite = 0b100,
        All = StaticAnalysis | DynamicAnalysis | AssemblyRewrite
    }
}