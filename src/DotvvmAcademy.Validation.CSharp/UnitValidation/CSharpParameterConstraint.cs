using System;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    [Flags]
    public enum CSharpParameterConstraint
    {
        None = 0,
        ParameterlessConstructor = 1,
        Class = 2,
        Struct = 7
    }
}