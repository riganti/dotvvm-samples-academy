using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class DesiredTypeKindExtensions
    {
        public static bool HasRoslynTypeKind(this DesiredTypeKind desired, TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Array:
                    return desired.HasFlag(DesiredTypeKind.Array);

                case TypeKind.Class:
                    return desired.HasFlag(DesiredTypeKind.Class);

                case TypeKind.Delegate:
                    return desired.HasFlag(DesiredTypeKind.Delegate);

                case TypeKind.Enum:
                    return desired.HasFlag(DesiredTypeKind.Enum);

                case TypeKind.Interface:
                    return desired.HasFlag(DesiredTypeKind.Interface);

                case TypeKind.Pointer:
                    return desired.HasFlag(DesiredTypeKind.Pointer);

                case TypeKind.Struct:
                    return desired.HasFlag(DesiredTypeKind.Struct);

                case TypeKind.TypeParameter:
                    return desired.HasFlag(DesiredTypeKind.TypeParameter);

                default:
                    return false;
            }
        }
    }
}