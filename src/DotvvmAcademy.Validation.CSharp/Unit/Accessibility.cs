using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    [Flags]
    public enum Accessibility
    {
        /// <summary>
        /// An invalid value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Stands for 'private'.
        /// </summary>
        Private = 1 << 0,

        /// <summary>
        /// Stands for 'private protected'.
        /// </summary>
        ProtectedAndInternal = 1 << 1,

        /// <summary>
        /// Stands for 'protected'.
        /// </summary>
        Protected = 1 << 2,

        /// <summary>
        /// Stands for 'internal'.
        /// </summary>
        Internal = 1 << 3,

        /// <summary>
        /// Stands for 'protected internal'.
        /// </summary>
        ProtectedOrInternal = 1 << 4,

        /// <summary>
        /// Stands for 'public'.
        /// </summary>
        Public = 1 << 5
    }
}