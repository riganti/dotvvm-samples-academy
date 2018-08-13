using UnitAccessibility = DotvvmAcademy.Validation.CSharp.Unit.Accessibility;

namespace Microsoft.CodeAnalysis
{
    public static class AccessibilityExtensions
    {
        public static UnitAccessibility ToUnitAccessibility(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return UnitAccessibility.Private;

                case Accessibility.ProtectedAndInternal:
                    return UnitAccessibility.ProtectedAndInternal;

                case Accessibility.Protected:
                    return UnitAccessibility.Protected;

                case Accessibility.Internal:
                    return UnitAccessibility.Internal;

                case Accessibility.ProtectedOrInternal:
                    return UnitAccessibility.ProtectedOrInternal;

                case Accessibility.Public:
                    return UnitAccessibility.Public;

                default:
                    return UnitAccessibility.None;
            }
        }
    }
}