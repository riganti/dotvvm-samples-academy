using DotvvmAcademy.Validation.CSharp.Unit;

namespace Microsoft.CodeAnalysis
{
    public static class AccessibilityExtensions
    {
        public static CSharpAccessibility ToCSharpAccessibility(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return CSharpAccessibility.Private;

                case Accessibility.ProtectedAndInternal:
                    return CSharpAccessibility.ProtectedAndInternal;
                    
                case Accessibility.Protected:
                    return CSharpAccessibility.Protected;

                case Accessibility.Internal:
                    return CSharpAccessibility.Internal;

                case Accessibility.ProtectedOrInternal:
                    return CSharpAccessibility.ProtectedOrInternal;

                case Accessibility.Public:
                    return CSharpAccessibility.Public;

                default:
                    return CSharpAccessibility.None;
            }
        }
    }
}