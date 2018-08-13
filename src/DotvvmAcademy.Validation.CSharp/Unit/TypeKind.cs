using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    [Flags]
    public enum TypeKind
    {
        None = 0,
        Class = 1 << 0,
        Delegate = 1 << 1,
        Enum = 1 << 2,
        Interface = 1 << 3,
        Struct = 1 << 4
    }
}