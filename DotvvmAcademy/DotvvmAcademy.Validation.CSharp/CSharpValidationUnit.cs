using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationUnit
    {
        public CSharpValidationUnit(CSharpSyntaxTree syntaxTree, ImmutableArray<string> validationMethods)
        {
            SyntaxTree = syntaxTree ?? throw new System.ArgumentNullException(nameof(syntaxTree));
            ValidationMethods = validationMethods;
        }

        public CSharpValidationUnit(CSharpSyntaxTree syntaxTree, IEnumerable<string> validationMethods) :
            this(syntaxTree, validationMethods.ToImmutableArray())
        {
        }

        public CSharpValidationUnit(CSharpSyntaxTree syntaxTree, params string[] validationMethods) :
            this(syntaxTree, (IEnumerable<string>)validationMethods)
        {
        }

        public CSharpSyntaxTree SyntaxTree { get; }

        public ImmutableArray<string> ValidationMethods { get; }
    }
}