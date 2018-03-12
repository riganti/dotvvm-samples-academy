using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum MetadataNameKind
    {
        None = 0,
        Type = 1 << 0,
        NestedType = 1 << 1,
        ArrayType = 1 << 2,
        PointerType = 1 << 3,
        ConstructedType = 1 << 4,
        TypeParameter = 1 << 5,
        Member = 1 << 6,
        Method = 1 << 7,
        ConstructedMethod = 1 << 8
    }
}