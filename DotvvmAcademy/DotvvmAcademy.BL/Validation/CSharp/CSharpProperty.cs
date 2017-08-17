using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpProperty : CSharpValidationObject<PropertyDeclarationSyntax>
    {
        public CSharpProperty(CSharpValidate validate, PropertyDeclarationSyntax node) : base(validate, node)
        {
        }

        public static CSharpProperty Inactive => new CSharpProperty(null, null) { IsActive = false };

        public void AccessModifier(CSharpAccessModifier access)
        {
            if (!IsActive) return;
        }

        public void Getter(CSharpAccessModifier access)
        {
            if (!IsActive) return;
        }

        public void Setter(CSharpAccessModifier access)
        {
            if (!IsActive) return;
        }
    }
}