using DotvvmAcademy.Validation.CSharp;

namespace Microsoft.CodeAnalysis
{
    public static class AccessiblityExtensions
    {
        public static AllowedAccess ToAllowedAccess(this Accessibility accessibility)
        {
            return accessibility switch
            {
                Accessibility.Private => AllowedAccess.Private,
                Accessibility.ProtectedAndInternal => AllowedAccess.ProtectedAndInternal,
                Accessibility.Protected => AllowedAccess.Protected,
                Accessibility.Internal => AllowedAccess.Internal,
                Accessibility.ProtectedOrInternal => AllowedAccess.ProtectedOrInternal,
                Accessibility.Public => AllowedAccess.Public,
                _ => AllowedAccess.None,
            };
        }
    }
}
