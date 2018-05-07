using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class DesiredTypeKindExtensions
    {
        public static bool HasRoslynTypeKind(this CSharpTypeKind desired, TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Array:
                    return desired.HasFlag(CSharpTypeKind.Array);

                case TypeKind.Class:
                    return desired.HasFlag(CSharpTypeKind.Class);

                case TypeKind.Delegate:
                    return desired.HasFlag(CSharpTypeKind.Delegate);

                case TypeKind.Enum:
                    return desired.HasFlag(CSharpTypeKind.Enum);

                case TypeKind.Interface:
                    return desired.HasFlag(CSharpTypeKind.Interface);

                case TypeKind.Pointer:
                    return desired.HasFlag(CSharpTypeKind.Pointer);

                case TypeKind.Struct:
                    return desired.HasFlag(CSharpTypeKind.Struct);

                case TypeKind.TypeParameter:
                    return desired.HasFlag(CSharpTypeKind.TypeParameter);

                default:
                    return false;
            }
        }
    }
}