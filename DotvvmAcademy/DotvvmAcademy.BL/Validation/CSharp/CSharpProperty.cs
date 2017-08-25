using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpProperty : CSharpObject<PropertyDeclarationSyntax>
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

        public override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.Start);

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

        public void Type(CSharpTypeDescriptor type)
        {
            if (!IsActive || !type.IsActive) return;

            if(!Symbol.Type.GetDescriptor().Equals(type))
            {
                AddError($"This property should be of type: '{type.GetFriendlyName()}'.");
            }
        }
    }
}