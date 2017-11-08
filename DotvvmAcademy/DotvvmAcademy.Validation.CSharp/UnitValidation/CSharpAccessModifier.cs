using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    /// <summary>
    /// The allowed access modifiers of <see cref="ICSharpAllowsAccessModifier"/>.
    /// </summary>
    public enum CSharpAccessModifier
    {
        Public = 0,
        Private,
        ProtectedInternal,
        Protected,
        Internal,
    }
}