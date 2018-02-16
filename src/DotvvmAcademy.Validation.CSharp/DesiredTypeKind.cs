using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum DesiredTypeKind
    {
        Array = 1 << 0,
        Class = 1 << 1,
        Delegate = 1 << 2,
        Enum = 1 << 3,
        Interface = 1 << 4,
        Pointer = 1 << 5,
        Struct = 1 << 6,
        TypeParameter = 1 << 7
    }
}