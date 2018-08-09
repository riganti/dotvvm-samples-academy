using UnitTypeKind = DotvvmAcademy.Validation.CSharp.Unit.TypeKind;

namespace Microsoft.CodeAnalysis
{
    public static class TypeKindExtensions
    {
        public static UnitTypeKind ToUnitTypeKind(this TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return UnitTypeKind.Class;

                case TypeKind.Delegate:
                    return UnitTypeKind.Delegate;

                case TypeKind.Enum:
                    return UnitTypeKind.Enum;

                case TypeKind.Interface:
                    return UnitTypeKind.Interface;

                case TypeKind.Struct:
                    return UnitTypeKind.Struct;

                default:
                    return UnitTypeKind.None;
            }
        }
    }
}