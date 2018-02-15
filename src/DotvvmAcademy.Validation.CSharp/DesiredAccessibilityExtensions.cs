using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class DesiredAccessibilityExtensions
    {
        public static bool HasRoslynAccessibility(this DesiredAccessiblity desired, Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return desired.HasFlag(DesiredAccessiblity.Private);

                case Accessibility.ProtectedAndInternal:
                    return desired.HasFlag(DesiredAccessiblity.ProtectedAndInternal);

                case Accessibility.Protected:
                    return desired.HasFlag(DesiredAccessiblity.Protected);

                case Accessibility.Internal:
                    return desired.HasFlag(DesiredAccessiblity.Internal);

                case Accessibility.ProtectedOrInternal:
                    return desired.HasFlag(DesiredAccessiblity.ProtectedOrInternal);

                case Accessibility.Public:
                    return desired.HasFlag(DesiredAccessiblity.Public);

                default:
                    return false;
            }
        }
    }
}