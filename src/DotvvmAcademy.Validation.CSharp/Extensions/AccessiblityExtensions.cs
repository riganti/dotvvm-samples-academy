using DotvvmAcademy.Validation.CSharp;

namespace Microsoft.CodeAnalysis
{
    public static class AccessiblityExtensions
    {
        public static AllowedAccess ToAllowedAccess(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return AllowedAccess.Private;

                case Accessibility.ProtectedAndInternal:
                    return AllowedAccess.ProtectedAndInternal;

                case Accessibility.Protected:
                    return AllowedAccess.Protected;

                case Accessibility.Internal:
                    return AllowedAccess.Internal;

                case Accessibility.ProtectedOrInternal:
                    return AllowedAccess.ProtectedOrInternal;

                case Accessibility.Public:
                    return AllowedAccess.Public;

                default:
                    return AllowedAccess.None;
            }
        }
    }
}