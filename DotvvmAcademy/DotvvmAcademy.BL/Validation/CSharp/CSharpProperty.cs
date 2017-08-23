using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpProperty : CSharpValidationObject<PropertyDeclarationSyntax>
    {
        internal CSharpProperty(CSharpValidate validate, PropertyDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
            if (!IsActive) return;

            Symbol = Validate.Model.GetDeclaredSymbol(Node);
        }

        public static CSharpProperty Inactive => new CSharpProperty(null, null, false);

        public IPropertySymbol Symbol { get; }

        public void AccessModifier(CSharpAccessModifier access)
        {
            if (!IsActive) return;

            if (Symbol.DeclaredAccessibility != access.ToCodeAnalysis())
            {
                AddError($"This property should be '{access.ToHumanReadable()}'.");
            }
        }

        public void Getter(CSharpAccessModifier access)
        {
            if (!IsActive) return;

            if (Symbol.GetMethod == null)
            {
                AddError($"This is missing a '{access.ToHumanReadable()}' getter.");
                return;
            }

            if (Symbol.GetMethod.DeclaredAccessibility != access.ToCodeAnalysis())
            {
                AddError($"This property should have a '{access.ToHumanReadable()}' getter.");
            }
        }

        public void NoGetter()
        {
            if (!IsActive) return;

            if (Symbol.GetMethod != null)
            {
                AddError("This property should not have a getter");
            }
        }

        public void NoSetter()
        {
            if (!IsActive) return;

            if (Symbol.SetMethod != null)
            {
                AddError("This property should not have a setter");
            }
        }

        public void Setter(CSharpAccessModifier access)
        {
            if (!IsActive) return;

            if (Symbol.SetMethod == null)
            {
                AddError($"This is missing a '{access.ToHumanReadable()}' setter.");
                return;
            }

            if (Symbol.SetMethod.DeclaredAccessibility != access.ToCodeAnalysis())
            {
                AddError($"This property should have a '{access.ToHumanReadable()}' setter.");
            }
        }

        protected override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.Start);
    }
}