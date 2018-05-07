using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class DesiredAccessibilityExtensions
    {
        public static bool HasRoslynAccessibility(this CSharpAccessibility desired, Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return desired.HasFlag(CSharpAccessibility.Private);

                case Accessibility.ProtectedAndInternal:
                    return desired.HasFlag(CSharpAccessibility.ProtectedAndInternal);

                case Accessibility.Protected:
                    return desired.HasFlag(CSharpAccessibility.Protected);

                case Accessibility.Internal:
                    return desired.HasFlag(CSharpAccessibility.Internal);

                case Accessibility.ProtectedOrInternal:
                    return desired.HasFlag(CSharpAccessibility.ProtectedOrInternal);

                case Accessibility.Public:
                    return desired.HasFlag(CSharpAccessibility.Public);

                default:
                    return false;
            }
        }
    }
}