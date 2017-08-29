using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class CSharpMethod : CSharpObject<MethodDeclarationSyntax>
    {
        internal CSharpMethod(CSharpValidate validate, MethodDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
            if (!IsActive) return;
            var model = Validate.Compilation.GetSemanticModel(Node.SyntaxTree);
            Symbol = model.GetDeclaredSymbol(Node);
        }

        public static CSharpMethod Inactive { get; } = new CSharpMethod(null, null, false);

        public IMethodSymbol Symbol { get; }

        public void AccessModifier(CSharpAccessModifier access)
        {
            if (!IsActive) return;

            if (Symbol.DeclaredAccessibility != access.ToCodeAnalysis())
            {
                AddError($"This method should be '{access.ToHumanReadable()}'.");
            }
        }

        public override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.End);
    }
}