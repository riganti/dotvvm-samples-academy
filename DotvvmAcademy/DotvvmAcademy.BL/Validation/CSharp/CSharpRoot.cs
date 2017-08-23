using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpRoot : CSharpValidationObject<CompilationUnitSyntax>
    {
        internal CSharpRoot(CSharpValidate validate, CompilationUnitSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public CSharpRoot Inactive => new CSharpRoot(null, null, false);

        public void Usings(params string[] expectedUsings)
        {
            if (!IsActive) return;

            var usersUsings = Node.Usings.Select(u => u.Name.ToString());
            var missingUsings = expectedUsings.Except(usersUsings);
            foreach(var missing in missingUsings)
            {
                AddError($"This file is missing the '{missing}' using.");
            }
            var extraUsings = usersUsings.Except(expectedUsings);
            foreach(var extra in extraUsings)
            {
                AddError($"This file contain an extra using: '{extra}'.");
            }
        }

        public CSharpNamespace Namespace(string name)
        {
            if (!IsActive) return CSharpNamespace.Inactive;

            var namespaces = Node.Members.OfType<NamespaceDeclarationSyntax>().Where(n => n.Name.ToString() == name).ToList();
            if(namespaces.Count > 1)
            {
                AddError($"This file should not contain multiple namespace declarations named '{name}'.");
                return CSharpNamespace.Inactive;
            }

            if(namespaces.Count == 0)
            {
                AddError($"This file is missing a namespace named '{name}'.");
                return CSharpNamespace.Inactive;
            }

            return new CSharpNamespace(Validate, namespaces.Single());
        }

        protected override void AddError(string message) => AddGlobalError(message);
    }
}
