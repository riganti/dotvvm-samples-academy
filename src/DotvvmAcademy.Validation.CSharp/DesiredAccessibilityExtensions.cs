using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class DesiredAccessibilityExtensions
    {
        public static bool HasRoslynAccessibility(this DesiredAccessibility desired, Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return desired.HasFlag(DesiredAccessibility.Private);

                case Accessibility.ProtectedAndInternal:
                    return desired.HasFlag(DesiredAccessibility.ProtectedAndInternal);

                case Accessibility.Protected:
                    return desired.HasFlag(DesiredAccessibility.Protected);

                case Accessibility.Internal:
                    return desired.HasFlag(DesiredAccessibility.Internal);

                case Accessibility.ProtectedOrInternal:
                    return desired.HasFlag(DesiredAccessibility.ProtectedOrInternal);

                case Accessibility.Public:
                    return desired.HasFlag(DesiredAccessibility.Public);

                default:
                    return false;
            }
        }
    }
}