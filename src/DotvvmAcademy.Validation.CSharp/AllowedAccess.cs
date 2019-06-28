using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [Flags]
    public enum AllowedAccess
    {
        /// <summary>
        /// Symbol must not be accessible.
        /// </summary>
        /// <remarks>
        /// Always produces a diagnostic.
        /// </remarks>
        None = 0,

        /// <summary>
        /// Symbol can be 'private'.
        /// </summary>
        Private = 1 << 0,

        /// <summary>
        /// Symbol can be 'private protected'.
        /// </summary>
        ProtectedAndInternal = 1 << 1,

        /// <summary>
        /// Symbol can be 'protected'.
        /// </summary>
        Protected = 1 << 2,

        /// <summary>
        /// Symbol can be 'internal'.
        /// </summary>
        Internal = 1 << 3,

        /// <summary>
        /// Symbol can be 'protected internal'.
        /// </summary>
        ProtectedOrInternal = 1 << 4,

        /// <summary>
        /// Symbol can be for 'public'.
        /// </summary>
        Public = 1 << 5,

        /// <summary>
        /// All access modifiers are allowed.
        /// </summary>
        All = ~0
    }
}