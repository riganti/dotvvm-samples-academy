using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed class CSharpNamespace : CSharpObject<NamespaceDeclarationSyntax>
    {
        internal CSharpNamespace(CSharpValidate validate, NamespaceDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static CSharpNamespace Inactive => new CSharpNamespace(null, null, false);

        public CSharpClass Class(string name)
        {
            if (!IsActive) return CSharpClass.Inactive;

            var classes = Node.Members.OfType<ClassDeclarationSyntax>().Where(c => c.Identifier.ValueText == name).ToList();
            if (classes.Count > 1)
            {
                AddError($"This namespace should not contain multiple classes called '{name}'.");
                return CSharpClass.Inactive;
            }

            if (classes.Count == 0)
            {
                AddError($"This namespace is missing the '{name}' class.");
                return CSharpClass.Inactive;
            }

            return new CSharpClass(Validate, classes.Single());
        }

        public override void AddError(string message)
        {
            AddError(message, Node.Name.Span.Start, Node.Name.Span.End);
        }
    }
}