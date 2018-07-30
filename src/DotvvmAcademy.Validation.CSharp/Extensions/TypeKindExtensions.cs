using DotvvmAcademy.Validation.CSharp.Unit;

namespace Microsoft.CodeAnalysis
{
    public static class TypeKindExtensions
    {
        public static CSharpTypeKind ToCSharpTypeKind(this TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return CSharpTypeKind.Class;

                case TypeKind.Delegate:
                    return CSharpTypeKind.Delegate;

                case TypeKind.Enum:
                    return CSharpTypeKind.Enum;

                case TypeKind.Interface:
                    return CSharpTypeKind.Interface;

                case TypeKind.Struct:
                    return CSharpTypeKind.Struct;

                default:
                    return CSharpTypeKind.None;
            }
        }
    }
}